﻿using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class DeclarationListNode : Node
    {
        public DeclarationListNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var declaredObjects = new HashSet<string>();
            var declaredTypes = new HashSet<string>();

            foreach (var node in Children.Cast<IDeclarationList>())
            {
                HashSet<string> s = node is TypeDeclListNode ? declaredTypes : declaredObjects;

                foreach (var decl in node.DeclaredNames)
                    if (s.Contains(decl))
                        errors.Add(new SemanticError
                        {
                            Message = string.Format("{0} '{1}' declared directly in the same 'let' several times",
                                                     node is TypeDeclListNode ? "Type" : "Variable/function",
                                                     decl),
                            Node = (Node)node
                        });
                    else
                        s.Add(decl);
            }

            if (errors.Any()) return;

            foreach (var node in Children)
            {
                if (errors.Any()) break;
                node.CheckSemantics(scope, errors);
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            Children.ForEach(n => n.Generate(generator));
        }
    }

    interface IDeclarationList
    {
        /// <summary>
        /// Name of declared elements in the sequence of declarations
        /// </summary>
        string[] DeclaredNames { get; }
    }
}

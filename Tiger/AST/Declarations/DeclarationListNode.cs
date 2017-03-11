using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class DeclarationListNode : Node
    {
        public DeclarationListNode(ParserRuleContext context) : base(context) { }

        public DeclarationListNode(int line, int column) : base(line, column) { }

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
                            Message = string.Format("{0} {1} declared directly in the same 'let' several times",
                                                     node is TypeDeclListNode ? "Type" : "Variable/function",
                                                     decl),
                            Node = (Node)node
                        });
                    else
                        s.Add(decl);
            }

            if (errors.Count > 0) return;

            foreach (var node in Children)
            {
                if (errors.Count > 0) break;
                node.CheckSemantics(scope, errors);
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var node in Children)
                node.Generate(generator);
        }
    }

    interface IDeclarationList
    {
        string[] DeclaredNames { get; }
    }
}

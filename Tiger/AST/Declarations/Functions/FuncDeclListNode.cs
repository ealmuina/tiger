using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System;

namespace Tiger.AST
{
    class FuncDeclListNode : Node, IDeclarationList
    {
        public FuncDeclListNode(ParserRuleContext context) : base(context) { }

        public string[] DeclaredNames
        {
            get
            {
                return (from f in Children.Cast<FuncDeclNode>()
                        select f.Name).ToArray();
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var functions = Children.Cast<FuncDeclNode>();

            foreach (var func in functions)
            {
                scope.DefineFunction(func.Name, func.FunctionType,
                    func.Arguments != null ? func.Arguments.Types : new string[] { });
            }

            foreach (var func in functions)
                func.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var func in Children.Cast<FuncDeclNode>())
                func.Define(generator);

            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

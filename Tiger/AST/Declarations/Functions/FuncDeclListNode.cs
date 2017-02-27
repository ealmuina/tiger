using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class FuncDeclListNode : Node
    {
        public FuncDeclListNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var functions = Children.Cast<FuncDeclNode>();

            foreach (var func in functions)
            {
                if (functions.Count(f => f.Name == func.Name) > 1)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Function '{0}' is declared in a functions declaration sequence several times", func.Name),
                        Node = this
                    });

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

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
    class DeclarationListNode : Node
    {
        public DeclarationListNode(ParserRuleContext context) : base(context) { }

        public DeclarationListNode(int line, int column) : base(line, column) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //Define Functions and Types at the start of their scopes
            foreach (var node in Children)
            {
                if (node is FuncDeclNode)
                {
                    var func = (FuncDeclNode)node;
                    if (scope.Stdl.Where(n => n.Name == func.Name).Count() > 0)
                        errors.Add(new SemanticError
                        {
                            Message = string.Format("Standard library function {0} can not be redefined", func.Name),
                            Node = Children[0]
                        });
                    else
                        scope.DefineFunction(func.Name, func.FunctionType,
                            func.Arguments != null ? func.Arguments.Types : new string[] { });
                }

                if (node is TypeDeclNode)
                    scope.DefineType((node as TypeDeclNode).Name);
            }

            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

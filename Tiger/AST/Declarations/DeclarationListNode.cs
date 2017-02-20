using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
            IEnumerable<DeclarationNode> varsAndFunctions = Children.Where(n => !(n is TypeDeclNode)).Cast<DeclarationNode>();
            IEnumerable<TypeDeclNode> decls = Children.Where(n => n is TypeDeclNode).Cast<TypeDeclNode>();

            foreach (var node in varsAndFunctions)
                if (varsAndFunctions.Count(n => n.Name == node.Name) > 1)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("{0} '{1}' is declared directly in a 'let' expression several times",
                                                (node is VarDeclNode) ? "Variable" : "Function",
                                                node.Name),
                        Node = this
                    });

            foreach (var node in decls)
                if (varsAndFunctions.Count(n => n.Name == node.Name) > 1)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Type '{0}' is declared directly in a 'let' expression several times", node.Name),
                        Node = this
                    });

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
                    (node as TypeDeclNode).DefineType(scope, errors);
            }

            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            //Define Functions and Types at the start of their scopes
            foreach (var node in Children)
            {
                if (node is FuncDeclNode)
                    (node as FuncDeclNode).Define(generator);

                if (node is TypeDeclNode)
                    (node as TypeDeclNode).Define(generator);
            }

            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

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
                    scope.DefineFunction(func.Name, func.FunctionType);
                }

                if (node is TypeDeclNode)
                    scope.DefineType((node as TypeDeclNode).Name);
            }

            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            foreach (var node in Children)
                node.Generate(generator, symbols);
        }
    }
}

using Antlr4.Runtime;
using System.Collections.Generic;
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
            //TODO Chequear que en la misma secuencia de declaraciones no se redefinan tipos o funciones/variables

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
}

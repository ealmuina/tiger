using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class NegativeNode : UnaryNode
    {
        public NegativeNode(ParserRuleContext context) : base(context) { }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            Operand.Generate(generator, symbols);
            //generator.Emit(OpCodes.Neg);
        }
    }
}

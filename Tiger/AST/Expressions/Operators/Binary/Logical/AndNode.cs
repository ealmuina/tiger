using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class AndNode : LogicalNode
    {
        public AndNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode => OpCodes.And;

        public override OpCode ShortCircuitOpCode => OpCodes.Ldc_I4_0;
    }
}

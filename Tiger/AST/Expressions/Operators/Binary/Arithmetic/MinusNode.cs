using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class MinusNode : ArithmeticNode
    {
        public MinusNode(ParserRuleContext context) : base(context) { }

        public override string OperatorName => "minus";

        public override OpCode OperatorOpCode => OpCodes.Sub;
    }
}

using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class DivNode : ArithmeticNode
    {
        public DivNode(ParserRuleContext context) : base(context) { }

        public override string OperatorName => "divide";

        public override OpCode OperatorOpCode => OpCodes.Div;
    }
}

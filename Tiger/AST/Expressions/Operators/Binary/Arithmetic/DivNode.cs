using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class DivNode : ArithmeticNode
    {
        public DivNode(ParserRuleContext context) : base(context) { }

        public override string OperatorName
        {
            get { return "divide"; }
        }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Div; }
        }
    }
}

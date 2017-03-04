using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class MulNode : ArithmeticNode
    {
        public MulNode(ParserRuleContext context) : base(context) { }

        public override string OperatorName
        {
            get { return "multiply"; }
        }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Mul; }
        }
    }
}

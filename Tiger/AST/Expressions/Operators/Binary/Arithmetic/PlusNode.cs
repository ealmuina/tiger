using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class PlusNode : ArithmeticNode
    {
        public PlusNode(ParserRuleContext context) : base(context) { }

        public override string OperatorName
        {
            get { return "plus"; }
        }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Add; }
        }
    }
}

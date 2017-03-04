using Antlr4.Runtime;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class OrNode : LogicalNode
    {
        public OrNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Or; }
        }

        public override OpCode ShortCircuitOpCode
        {
            get { return OpCodes.Ldc_I4_1; }
        }
    }
}

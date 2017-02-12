using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class AndNode : LogicalNode
    {
        public AndNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.And; }
        }

        public override OpCode ShortCircuitOpCode
        {
            get { return OpCodes.Ldc_I4_0; }
        }
    }
}

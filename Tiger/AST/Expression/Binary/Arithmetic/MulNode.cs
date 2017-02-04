using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGen;

namespace Tiger.AST
{
    class MulNode : ArithmeticNode
    {
        public MulNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Mul; }
        }
    }
}

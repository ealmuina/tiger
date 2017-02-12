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

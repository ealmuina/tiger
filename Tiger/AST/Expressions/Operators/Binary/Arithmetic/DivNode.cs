﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.AST
{
    class DivNode : ArithmeticNode
    {
        public DivNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Div; }
        }
    }
}
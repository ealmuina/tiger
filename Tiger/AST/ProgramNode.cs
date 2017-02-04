﻿using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    abstract class ProgramNode : Node
    {
        public ProgramNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Expression { get; protected set; }
    }
}

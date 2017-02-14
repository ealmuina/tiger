﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class IdNode : AuxiliaryNode
    {
        public IdNode(ParserRuleContext context, string name) : base(context)
        {
            Name = name;
        }

        public IdNode(int line, int column, string name) : base(line, column)
        {
            Name = name;
        }

        public string Name { get; protected set; }
    }
}
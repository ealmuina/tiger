﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class EqNode : ComparisonNode
    {
        public EqNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Ceq; }
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            throw new NotImplementedException();
        }
    }
}

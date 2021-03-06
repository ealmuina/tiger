﻿using System.Linq;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    class LtNode : ComparisonNode
    {
        public LtNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(TypeInfo type)
        {
            return new[] { Types.Int, Types.String }.Contains(type);
        }

        protected override void CompareInt(ILGenerator il)
        {
            il.Emit(OpCodes.Clt);
        }

        protected override void CompareString(ILGenerator il)
        {
            base.CompareString(il);
            il.Emit(OpCodes.Ldc_I4, 0);
            il.Emit(OpCodes.Clt);
        }

        protected override void CompareOther(ILGenerator il) { }
    }
}

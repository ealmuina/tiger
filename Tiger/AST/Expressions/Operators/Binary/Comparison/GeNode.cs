﻿using System.Linq;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    class GeNode : ComparisonNode
    {
        public GeNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(TypeInfo type)
        {
            return new[] { Types.Int, Types.String }.Contains(type);
        }

        protected override void CompareInt(ILGenerator il)
        {
            //The relation >= is true iff < is false
            il.Emit(OpCodes.Clt);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
        }

        protected override void CompareString(ILGenerator il)
        {
            base.CompareString(il);
            il.Emit(OpCodes.Ldc_I4, -1);
            il.Emit(OpCodes.Cgt);
        }

        protected override void CompareOther(ILGenerator il) { }
    }
}

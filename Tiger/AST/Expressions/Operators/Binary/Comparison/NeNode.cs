using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NeNode : ComparisonNode
    {
        public NeNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(string type)
        {
            return type != Types.Void;
        }

        protected override void CompareInt(ILGenerator il)
        {
            //The are different if they are not equals
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
        }

        protected override void CompareString(ILGenerator il)
        {
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            // if comparison result is 0 they are not equals
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
        }

        protected override void CompareOther(ILGenerator il)
        {
            //Same as comparing integers
            CompareInt(il);
        }
    }
}

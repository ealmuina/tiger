using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NeNode : EqNode
    {
        public NeNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(TypeInfo type)
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
            base.CompareString(il);
            //just invert equality comparison's result
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

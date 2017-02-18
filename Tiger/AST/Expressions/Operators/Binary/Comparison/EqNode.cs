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
    class EqNode : ComparisonNode
    {
        public EqNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(string type)
        {
            return type != Types.Void;
        }

        protected override void CompareInt(ILGenerator il)
        {
            il.Emit(OpCodes.Ceq);
        }

        protected override void CompareString(ILGenerator il)
        {
            Label firstNil = il.DefineLabel();
            Label end = il.DefineLabel();

            LocalBuilder second = il.DeclareLocal(typeof(string));
            il.Emit(OpCodes.Stloc, second);

            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Beq, firstNil);

            //first operand is not 'nil', just compare using string's CompareTo
            il.Emit(OpCodes.Ldloc, second);
            base.CompareString(il);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Br, end);

            //first operand is 'nil' just compare it with second one
            il.MarkLabel(firstNil);
            il.Emit(OpCodes.Ldloc, second);
            il.Emit(OpCodes.Ceq);
                       
            il.MarkLabel(end);
        }

        protected override void CompareOther(ILGenerator il)
        {
            //Same as comparing integers
            CompareInt(il);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using System.Reflection;
using Tiger.Semantics;

namespace Tiger.AST
{
    class LeNode : ComparisonNode
    {
        public LeNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(string type)
        {
            return new[] { Types.Int, Types.String }.Contains(type);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);
            RightOperand.Generate(generator, symbols);

            ILGenerator il = generator.Generator;

            if (Type == Types.String)
            {
                //Compare the strings using the string CompareTo method
                MethodInfo compareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                il.Emit(OpCodes.Call, compareTo);
                il.Emit(OpCodes.Ldc_I4, 1);
                il.Emit(OpCodes.Clt);
            }
            else
            {
                //The relation <= is true iff > is false
                il.Emit(OpCodes.Cgt);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
            }
        }
    }
}

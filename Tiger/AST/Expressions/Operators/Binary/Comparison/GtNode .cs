using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using System.Reflection;

namespace Tiger.AST
{
    class GtNode : ComparisonNode
    {
        public GtNode(ParserRuleContext context) : base(context) { }

        protected override bool SupportType(string type)
        {
            return new[] { "Int", "String" }.Contains(type);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);
            RightOperand.Generate(generator, symbols);

            ILGenerator il = generator.Generator;

            if (Type == "String")
            {
                //Compare the strings using the string CompareTo method
                MethodInfo compareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                il.Emit(OpCodes.Call, compareTo);
                il.Emit(OpCodes.Ldc_I4, 0);
                il.Emit(OpCodes.Cgt);
            }
            else
            {
                il.Emit(OpCodes.Cgt);
            }
        }
    }
}

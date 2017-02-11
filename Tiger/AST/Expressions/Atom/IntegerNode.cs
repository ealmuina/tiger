using System;
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
    class IntegerNode : AtomNode
    {
        public IntegerNode(ParserRuleContext context, string text) : base(context)
        {
            Value = int.Parse(text);
        }

        public override string Type
        {
            get { return "Int"; }
        }

        public int Value { get; protected set; }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            generator.Generator.Emit(OpCodes.Ldc_I4, Value);
        }
    }
}

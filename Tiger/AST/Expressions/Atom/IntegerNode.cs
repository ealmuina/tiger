using System.Reflection.Emit;
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
            Type = Types.Int;
        }

        public int Value { get; }

        public override void Generate(CodeGenerator generator) => generator.Generator.Emit(OpCodes.Ldc_I4, Value);
    }
}

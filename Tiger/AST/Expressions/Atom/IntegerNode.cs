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
    class IntegerNode : ExpressionNode
    {
        public IntegerNode(ParserRuleContext context, string text) : base(context)
        {
            Value = int.Parse(text);
        }

        public int Value { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }

        public override void Generate(CodeGenerator generator)
        {
            generator.Generator.Emit(OpCodes.Ldc_I4, Value);
        }
    }
}

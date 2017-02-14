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

        public override void Generate(CodeGenerator generator)
        {
            LeftOperand.Generate(generator);
            RightOperand.Generate(generator);
            generator.Generator.Emit(OpCodes.Ceq);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class BreakNode : FlowControlNode
    {
        public BreakNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (!scope.InsideLoop)
                errors.Add(new SemanticError
                {
                    Message = string.Format("break used out of a while or for statement"),
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            generator.Generator.Emit(OpCodes.Br, generator.LoopEnd);
        }
    }
}

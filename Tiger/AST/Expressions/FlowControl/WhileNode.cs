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
    class WhileNode : FlowControlNode
    {
        public WhileNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return Types.Void; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children[0].CheckSemantics(scope, errors);
            scope.InsideLoop = true;
            Children[1].CheckSemantics(scope, errors);

            if (Children[0].Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Invalid type of condition of the while statement"),
                    Node = Children[0]
                });

            if (Children[1].Type != Types.Void)
                errors.Add(new SemanticError
                {
                    Message = string.Format("while used with an expression with return value"),
                    Node = Children[0]
                });

            scope.InsideLoop = false;
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            Label condition = il.DefineLabel();
            Label end = il.DefineLabel();

            Label loopEnd = generator.LoopEnd; //store current loopEnd so we can restore it later
            generator.LoopEnd = end;

            //Check condition
            il.MarkLabel(condition);
            Children[0].Generate(generator);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, end);

            //true, exec body
            Children[1].Generate(generator);
            il.Emit(OpCodes.Br, condition);

            //false, end
            il.MarkLabel(end);

            generator.LoopEnd = loopEnd;
        }
    }
}

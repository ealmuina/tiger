using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class WhileNode : FlowControlNode
    {
        public WhileNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Condition
        {
            get { return Children[0] as ExpressionNode; }
        }

        public ExpressionNode Expression
        {
            get { return Children[1] as ExpressionNode; }
        }

        public override string Type
        {
            get { return Types.Void; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Condition.CheckSemantics(scope, errors);
            bool wasInside = scope.InsideLoop;
            scope.InsideLoop = true;
            Expression.CheckSemantics(scope, errors);

            if (Condition.Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = $"Invalid type of condition of the while statement",
                    Node = Condition
                });

            if (Expression.Type != Types.Void)
                errors.Add(new SemanticError
                {
                    Message = $"'while' used with an expression with return value",
                    Node = Expression
                });

            scope.InsideLoop = wasInside;
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
            Condition.Generate(generator);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, end);

            //true, exec body
            Expression.Generate(generator);
            il.Emit(OpCodes.Br, condition);

            //false, end
            il.MarkLabel(end);

            generator.LoopEnd = loopEnd;
        }
    }
}

using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Linq;

namespace Tiger.AST
{
    class ForNode : FlowControlNode
    {
        public ForNode(ParserRuleContext context) : base(context) { }

        public VarDeclNode Cursor
        {
            get => Children[0] as VarDeclNode;
        }

        public ExpressionNode ToExpression
        {
            get => Children[1] as ExpressionNode;
        }

        public ExpressionNode DoExpression
        {
            get => Children[2] as ExpressionNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var clone = new Scope(scope)
            {
                InsideLoop = true
            };

            Cursor.CheckSemantics(clone, errors);
            ToExpression.CheckSemantics(scope, errors);
            DoExpression.CheckSemantics(clone, errors);

            if (errors.Any()) return;

            if (Cursor.Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = $"The return type '{Cursor.Type}' of the expression for the lower bound of the 'for' loop is not integer",
                    Node = Children[0]
                });

            if (ToExpression.Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = "The expression for the upper bound of the 'for' loop does not return a value",
                    Node = Children[1]
                });

            if (DoExpression.Type != Types.Void)
                errors.Add(new SemanticError
                {
                    Message = "The body expression of the 'for' loop may not produce a result",
                    Node = Children[2]
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            generator = new CodeGenerator(generator);
            ILGenerator il = generator.Generator;

            Cursor.Generate(generator);
            LocalBuilder cursor = generator.Variables[Cursor.Name];

            LocalBuilder top = il.DeclareLocal(typeof(int));
            ToExpression.Generate(generator);
            il.Emit(OpCodes.Stloc, top);

            Label condition = il.DefineLabel();
            Label end = il.DefineLabel();

            Label loopEnd = generator.LoopEnd; //store current loopEnd so we can restore it later
            generator.LoopEnd = end;

            //Check if upper bound was reached
            il.MarkLabel(condition);

            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldloc, top);
            il.Emit(OpCodes.Bgt, end);

            //For body
            DoExpression.Generate(generator);

            //Increase cursor value and continue iteration
            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, cursor);
            il.Emit(OpCodes.Br, condition);

            //end
            il.MarkLabel(end);
            generator.LoopEnd = loopEnd;
        }
    }
}

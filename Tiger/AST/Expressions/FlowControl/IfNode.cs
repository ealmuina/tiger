using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class IfNode : FlowControlNode
    {
        public IfNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get => ElseExpression == null ? Types.Void : ElseExpression.Type;
        }

        public ExpressionNode IfExpression
        {
            get => Children[0] as ExpressionNode;
        }

        public ExpressionNode ThenExpression
        {
            get => Children[1] as ExpressionNode;
        }

        public ExpressionNode ElseExpression
        {
            get => Children[2] as ExpressionNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var expr in Children.Where(e => e != null))
                expr.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            if (!scope.SameType(IfExpression.Type, Types.Int))
                errors.Add(new SemanticError
                {
                    Message = "The condition of the 'if-then-else' statement does not return an integer value",
                    Node = IfExpression
                });

            if (ElseExpression == null && ThenExpression.Type != Types.Void) //if-then
                errors.Add(new SemanticError
                {
                    Message = "The 'then' expression of the 'if-then' statement should not return a value",
                    Node = ThenExpression
                });

            bool visibleType = true;

            if (!scope.IsDefined<TypeInfo>(ThenExpression.Type))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Value returned by the 'then' expression has a type {ThenExpression.Type} that isn't visible in the outer scope",
                    Node = ThenExpression
                });
                visibleType = false;
            }

            if (ElseExpression != null && !scope.IsDefined<TypeInfo>(ElseExpression.Type))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Value returned by the 'else' expression has a type {ElseExpression.Type} that isn't visible in the outer scope",
                    Node = ElseExpression
                });
                visibleType = false;
            }

            if (visibleType && ElseExpression != null && !scope.SameType(ThenExpression.Type, ElseExpression.Type)) //if-then-else
                errors.Add(new SemanticError
                {
                    Message = "The return types of the expressions of the 'if-then-else' statement are not the same",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            IfExpression.Generate(generator);

            ILGenerator il = generator.Generator;
            Label _else = il.DefineLabel();
            Label end = il.DefineLabel();

            // Check <if expression>
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, _else);

            //true
            ThenExpression.Generate(generator);
            il.Emit(OpCodes.Br, end); //skip false expr

            //false
            il.MarkLabel(_else);
            ElseExpression?.Generate(generator);

            il.MarkLabel(end);
        }
    }
}

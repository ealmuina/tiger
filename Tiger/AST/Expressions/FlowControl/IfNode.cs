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
            get => Children[2] == null ? Types.Void : Children[2].Type;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var expr in Children.Where(e => e != null))
                expr.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            if (Children[0].Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = $"The condition of the 'if-then-else' statement does not return an integer value",
                    Node = Children[0]
                });

            if (Children[2] == null && Children[1].Type != Types.Void) //if-then
                errors.Add(new SemanticError
                {
                    Message = $"The 'then' expression of the 'if-then' statement should not return a value",
                    Node = Children[1]
                });

            if (Children[2] != null && Children[1].Type != Children[2].Type) //if-then-else
                errors.Add(new SemanticError
                {
                    Message = $"The return types of the expressions of the 'if-then-else' statement are not the same",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            Children[0].Generate(generator);

            ILGenerator il = generator.Generator;
            Label _else = il.DefineLabel();
            Label end = il.DefineLabel();

            // Check <if expression>
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, _else);

            //true
            Children[1].Generate(generator);
            il.Emit(OpCodes.Br, end); //skip false expr

            //false
            il.MarkLabel(_else);
            if (Children[2] != null)
                Children[2].Generate(generator);

            il.MarkLabel(end);
        }
    }
}

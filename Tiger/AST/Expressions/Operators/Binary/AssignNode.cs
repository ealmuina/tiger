using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;
using System.Linq;

namespace Tiger.AST
{
    class AssignNode : ExpressionNode
    {
        public AssignNode(ParserRuleContext context) : base(context) { }

        public AssignNode(int line, int column) : base(line, column) { }

        public LValueNode LValue
        {
            get => Children[0] as LValueNode;
        }

        public ExpressionNode Expression
        {
            get => Children[1] as ExpressionNode;
        }

        public VariableInfo SymbolInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));
            if (errors.Any()) return;

            if (Expression.Type == Types.Void)
                errors.Add(new SemanticError
                {
                    Message = "Expression being assigned does not return a value",
                    Node = Expression
                });

            else if (LValue.Type != Expression.Type)
                errors.Add(new SemanticError
                {
                    Message = $"Incompatible types '{LValue.Type}' and '{Expression.Type}' for assignation",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            LValue.Generate(generator);
            Expression.Generate(generator);
            generator.Generator.Emit(OpCodes.Stobj, generator.Types[LValue.Type]);
        }
    }
}

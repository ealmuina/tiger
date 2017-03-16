using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class ProgramNode : Node
    {
        public ProgramNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Expression { get { return (ExpressionNode)Children[0]; } }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Expression.CheckSemantics(scope, errors);

            if (errors.Count == 0 && !scope.Types.ContainsKey(Expression.Type))
                errors.Add(new SemanticError
                {
                    Message = $"Type '{Expression.Type}' returned by the expression isn't visible in its context",
                    Node = Expression
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            Expression.Generate(generator);
            if (Expression.Type != Types.Void)
                generator.Generator.Emit(OpCodes.Pop);
            generator.Generator.Emit(OpCodes.Ret);
        }
    }
}

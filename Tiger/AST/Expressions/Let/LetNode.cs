using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class LetNode : ExpressionNode
    {
        public LetNode(ParserRuleContext context) : base(context) { }

        public LetNode(int line, int column) : base(line, column) { }

        public override string Type
        {
            get => Children.Last().Type;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Scope outerScope = scope;
            scope = new Scope(scope);

            Children.ForEach(n => n.CheckSemantics(scope, errors));
            if (errors.Count > 0) return;

            if (!outerScope.IsDefined<TypeInfo>(Type) || scope.GetItem<TypeInfo>(Type) != outerScope.GetItem<TypeInfo>(Type))
                errors.Add(new SemanticError
                {
                    Message = $"'let' block return type '{Type}' is not visible in the outer scope",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            generator = new CodeGenerator(generator);
            Children.ForEach(n => n.Generate(generator));
        }
    }
}

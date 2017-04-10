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

        public DeclarationListNode DeclarationList
        {
            get => Children[0] as DeclarationListNode;
        }

        public ExpressionSeqNode ExpressionSeq
        {
            get => Children[1] as ExpressionSeqNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Scope outerScope = scope;
            scope = new Scope(scope);

            Children.ForEach(n => n.CheckSemantics(scope, errors));
            if (errors.Any()) return;

            Type = Children.Last().Type;

            if (!outerScope.IsDefined<TypeInfo>(Type.Name) || Type != outerScope.GetItem<TypeInfo>(Type.Name))
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

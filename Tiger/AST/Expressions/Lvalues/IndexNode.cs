using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Linq;

namespace Tiger.AST
{
    class IndexNode : LValueNode
    {
        public IndexNode(ParserRuleContext context) : base(context) { }

        public LValueNode LValue
        {
            get => Children[0] as LValueNode;
        }

        public ExpressionNode IndexExpression
        {
            get => Children[1] as ExpressionNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Any()) return;

            var info = LValue.Type;

            if (!(info is ArrayInfo arrayInfo))
                errors.Add(new SemanticError
                {
                    Message = "Indexing operation requires Array type",
                    Node = this
                });
            else
                Type = arrayInfo.ElementsType;

            if (IndexExpression.Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = $"Invalid array indexing expression of type '{IndexExpression.Type}', it should be '{Types.Int}'",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            Type type = generator.Types[Type];

            LValue.Generate(generator);
            IndexExpression.Generate(generator);

            if (ByValue)
                il.Emit(OpCodes.Ldelem, type);
            else
                il.Emit(OpCodes.Ldelema, type);
        }
    }
}

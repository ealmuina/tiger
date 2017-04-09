using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class IndexNode : LValueNode
    {
        public IndexNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Expression
        {
            get => Children[1] as ExpressionNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            var info = Children[0].Type;

            if (!(info is ArrayInfo arrayInfo))
                errors.Add(new SemanticError
                {
                    Message = $"Indexing operation requires Array type",
                    Node = this
                });
            else
                Type = arrayInfo.ElementsType;

            if (Expression.Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = $"Invalid array indexing expression of type '{Expression.Type}', it should be '{Types.Int}'",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            Type type = generator.Types[Type];

            Children[0].Generate(generator);
            Expression.Generate(generator);

            if (ByValue)
                il.Emit(OpCodes.Ldelem, type);
            else
                il.Emit(OpCodes.Ldelema, type);
        }
    }
}

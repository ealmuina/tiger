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
        string type;

        public IndexNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return type; }
        }

        public ExpressionNode Expression
        {
            get { return Children[1] as ExpressionNode; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            var info = scope.GetItem<TypeInfo>(Children[0].Type);
            type = info.Name;

            if (!scope.SameType(Expression.Type, Types.Int))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Invalid array indexing expression of type '{0}', it should be '{1}'",
                                            Expression.Type, Types.Int),
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

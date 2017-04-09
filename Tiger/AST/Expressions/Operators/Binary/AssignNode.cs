﻿using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class AssignNode : ExpressionNode
    {
        public AssignNode(ParserRuleContext context) : base(context) { }

        public AssignNode(int line, int column): base(line, column) { }

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
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            if (Expression.Type == Types.Void)
                errors.Add(new SemanticError
                {
                    Message = "Expression being assigned does not return a value",
                    Node = Expression
                });

            if (!scope.IsDefined<TypeInfo>(Expression.Type))
                errors.Add(new SemanticError
                {
                    Message = $"Expression type {Expression.Type} doesn't exist in the current context",
                    Node = Expression
                });

            else if (!scope.SameType(LValue.Type, Expression.Type))
                errors.Add(new SemanticError
                {
                    Message = "Incompatible types for assignation",
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

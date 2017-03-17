﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class FuncallNode : ExpressionNode
    {
        public FuncallNode(ParserRuleContext context) : base(context) { }

        public string FunctionName
        {
            get => (Children[0] as IdNode).Name;
        }

        public IEnumerable<ExpressionNode> Arguments
        {
            get => Children.Skip(1).Cast<ExpressionNode>();
        }

        public FunctionInfo SymbolInfo { get; protected set; }

        public override string Type
        {
            get => SymbolInfo != null ? SymbolInfo.Type : Types.Void;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var arg in Arguments)
                arg.CheckSemantics(scope, errors);

            if (errors.Count > 0)
                return;

            if (!scope.IsDefined(FunctionName))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Function '{FunctionName}' does not exist",
                    Node = this
                });
                return;
            }

            ItemInfo info = scope[FunctionName];
            if (!(info is FunctionInfo fInfo))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Variable or constant '{FunctionName}' is being used as a function",
                    Node = this
                });
                return;
            }

            int parameterCount = fInfo.Parameters.Length;
            int argumentCount = Arguments.Count();
            if (parameterCount != argumentCount)
            {
                errors.Add(new SemanticError
                {
                    Message = $"Function '{FunctionName}' takes {parameterCount} arguments, got {argumentCount} instead",
                    Node = this
                });
                return;
            }

            var arguments = Arguments.ToArray();
            for (int i = 0; i < parameterCount; i++)
            {
                string expectedT = fInfo.Parameters[i];
                string exprT = arguments[i].Type;

                if (exprT != Types.Nil && !scope.SameType(exprT, expectedT))
                    errors.Add(new SemanticError
                    {
                        Message = $"Called function {FunctionName} with argument type '{exprT}' when expecting '{expectedT}'",
                        Node = arguments[i]
                    });
            }

            SymbolInfo = fInfo;
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            foreach (var arg in Arguments)
            {
                arg.Generate(generator);
            }

            if (!SymbolInfo.IsStdlFunc)
                foreach (var fv in SymbolInfo.ForeignVars)
                {
                    if (generator.Variables.ContainsKey(fv))
                    {
                        if (!SymbolInfo.Parameters.Contains(fv))
                            il.Emit(OpCodes.Ldloca, generator.Variables[fv]);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldarg, generator.ParamIndex[fv]);
                    }
                }

            il.Emit(OpCodes.Call, generator.Functions[SymbolInfo.Name]);
        }
    }
}

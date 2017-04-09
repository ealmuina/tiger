using System.Collections.Generic;
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

        public string Name
        {
            get => (Children[0] as IdNode).Name;
        }

        public List<ExpressionNode> Arguments
        {
            get => Children.Skip(1).Cast<ExpressionNode>().ToList();
        }

        public FunctionInfo SymbolInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Arguments.ForEach(a => a.CheckSemantics(scope, errors));

            if (errors.Count > 0)
                return;

            if (scope.IsDefined<VariableInfo>(Name))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Variable '{Name}' is being used as a function",
                    Node = this
                });
                return;
            }
            else if (!scope.IsDefined<FunctionInfo>(Name))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Function '{Name}' does not exist",
                    Node = this
                });
                return;
            }
            else
            {
                var info = scope.GetItem<FunctionInfo>(Name);

                int parameterCount = info.Parameters.Length;
                int argumentCount = Arguments.Count();
                if (parameterCount != argumentCount)
                {
                    errors.Add(new SemanticError
                    {
                        Message = $"Function '{Name}' takes {parameterCount} arguments, got {argumentCount} instead",
                        Node = this
                    });
                    return;
                }

                var arguments = Arguments.ToArray();
                for (int i = 0; i < parameterCount; i++)
                {
                    TypeInfo expectedT = info.Parameters[i];
                    TypeInfo exprT = arguments[i].Type;

                    if (!exprT.Equals(Types.Nil) && exprT != expectedT)
                        errors.Add(new SemanticError
                        {
                            Message = $"Called function {Name} with argument type '{exprT}' when expecting '{expectedT}'",
                            Node = arguments[i]
                        });
                }

                SymbolInfo = info;
                Type = info.Type;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            Arguments.ForEach(a => a.Generate(generator));

            if (!SymbolInfo.IsStdlFunc) // stdl functions doesn't have closures
                foreach (var fv in SymbolInfo.ForeignVars) // pass foreign variables as parameters
                {
                    if (generator.Variables.ContainsKey(fv))
                    {
                        // fv is local in the calling context
                        il.Emit(OpCodes.Ldloca, generator.Variables[fv]);
                    }
                    else
                    {
                        // fv is foreign in the calling context
                        il.Emit(OpCodes.Ldarg, generator.ParamIndex[fv]);
                    }
                }

            il.Emit(OpCodes.Call, generator.Functions[SymbolInfo.Name]);
        }
    }
}

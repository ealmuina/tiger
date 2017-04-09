using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class VarAccessNode : LValueNode
    {
        public VarAccessNode(ParserRuleContext context, string name) : base(context)
        {
            Name = name;
        }

        public VarAccessNode(int line, int column, string name) : base(line, column)
        {
            Name = name;
        }

        public string Name { get; }

        public VariableInfo Info { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (!scope.IsDefined<VariableInfo>(Name))
            {
                errors.Add(new SemanticError
                {
                    Message = $"Undefined variable {Name}",
                    Node = this
                });

                if (scope.IsDefined<FunctionInfo>(Name))
                    errors.Add(new SemanticError
                    {
                        Message = $"Function '{Name}' used as variable",
                        Node = this
                    });
            }
            else
            {
                var info = scope.GetItem<VariableInfo>(Name);

                if (info.IsReadOnly && !ByValue)
                    errors.Add(new SemanticError
                    {
                        Message = $"Invalid use of assignment to a readonly variable",
                        Node = this
                    });

                Info = info;
                Type = info.Type;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            if (Info.IsForeign)
                il.Emit(OpCodes.Ldarg, generator.ParamIndex[Name]);
            else
                il.Emit(OpCodes.Ldloca, generator.Variables[Name]);

            if (ByValue)
            {
                il.Emit(OpCodes.Ldobj, generator.Types[Type]);
            }
        }
    }
}

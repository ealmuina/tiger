﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class VarAccessNode : LValueNode
    {
        string type;

        public VarAccessNode(ParserRuleContext context, string name) : base(context)
        {
            Name = name;
        }

        public VarAccessNode(int line, int column, string name) : base(line, column)
        {
            Name = name;
        }

        public string Name { get; protected set; }

        public bool IsParam { get; protected set; }

        public override string Type
        {
            get { return type; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (!scope.IsDefined<VariableInfo>(Name))
            {
                errors.Add(new SemanticError
                {
                    Message = string.Format("Undefined variable {0}", Name),
                    Node = this
                });

                if (scope.IsDefined(Name))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Function '{0}' used as variable", Name),
                        Node = this
                    });
            }
            else
            {
                var info = (VariableInfo)scope[Name];

                if (info.IsReadOnly && !IsAccessor)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Invalid use of assignment to a readonly variable"),
                        Node = this
                    });

                type = info.Type;
                IsParam = info.IsParam;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            if (IsParam)
                il.Emit(OpCodes.Ldarg, generator.ParamIndex[Name]);
            else
            {
                LocalBuilder variable = generator.Variables[Name];
                if (IsAccessor)
                    il.Emit(OpCodes.Ldloc, variable);
                else
                    il.Emit(OpCodes.Stloc, variable);
            }
        }
    }
}
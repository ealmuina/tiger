using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class FieldAccessNode : LValueNode
    {
        public FieldAccessNode(ParserRuleContext context) : base(context) { }

        public LValueNode LValue
        {
            get => Children[0] as LValueNode;
        }

        public string FieldName
        {
            get => (Children[1] as IdNode).Name;
        }

        public RecordInfo RecordInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Any()) return;

            var info = LValue.Type;

            if (!(info is RecordInfo recInfo))
                errors.Add(new SemanticError
                {
                    Message = $"Type '{info.Name}' isn't a record type",
                    Node = this
                });
            else
            {
                RecordInfo = recInfo;

                if (!RecordInfo.FieldNames.Contains(FieldName))
                    errors.Add(new SemanticError
                    {
                        Message = $"Type '{RecordInfo.Name}' doesn't have a field named '{FieldName}'",
                        Node = this
                    });
                else
                    Type = RecordInfo.FieldTypes[Array.IndexOf(RecordInfo.FieldNames, FieldName)];
            }            
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            FieldBuilder field = generator.Fields[RecordInfo][FieldName];

            LValue.Generate(generator);

            if (ByValue)
                il.Emit(OpCodes.Ldfld, field);
            else
                il.Emit(OpCodes.Ldflda, field);
        }
    }
}

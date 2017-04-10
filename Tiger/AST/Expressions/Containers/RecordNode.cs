using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class RecordNode : ExpressionNode
    {
        public RecordNode(ParserRuleContext context) : base(context) { }

        public string TypeName
        {
            get => (Children[0] as IdNode).Name;
        }

        public RecordInfo RecordInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));

            if (!scope.IsDefined<TypeInfo>(TypeName) || !(scope.GetItem<TypeInfo>(TypeName) is RecordInfo info))
                errors.Add(new SemanticError
                {
                    Message = $"Cannot instantiate the undefined record type '{TypeName}'",
                    Node = this
                });
            else
            {
                RecordInfo = info;
                Type = RecordInfo;

                if (Children.Count - 1 != info.FieldNames.Length)
                    errors.Add(new SemanticError
                    {
                        Message = "Invalid number of fields in record literal",
                        Node = this
                    });
                else
                    for (int i = 1; i < Children.Count; i++)
                    {
                        var field = (FieldNode)Children[i];

                        if (field.Name != info.FieldNames[i - 1])
                            errors.Add(new SemanticError
                            {
                                Message = $"Assigned field '{field.Name}' when expecting {info.FieldNames[i - 1]}",
                                Node = Children[i]
                            });

                        if (Children[i].Type != info.FieldTypes[i - 1])
                            errors.Add(new SemanticError
                            {
                                Message = $"Expression of type '{Children[i].Type}' cannot be assigned to field " +
                                          $"'{info.FieldNames[i - 1]}' with type '{info.FieldTypes[i - 1]}'",
                                Node = Children[i]
                            });
                    }
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            il.Emit(OpCodes.Newobj, generator.Types[RecordInfo].GetConstructor(new Type[] { })); // create record

            // Assign values to fields
            for (int i = 1; i < Children.Count; i++) // 0 is the record type name
            {
                var field = (FieldNode)Children[i];
                il.Emit(OpCodes.Dup); // keep record on evaluation stack
                field.Generate(generator);
                il.Emit(OpCodes.Stfld, generator.Fields[RecordInfo][field.Name]);
            }
        }
    }
}

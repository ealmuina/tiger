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

        public override string Type
        {
            get => (Children[0] as IdNode).Name;
        }

        public RecordInfo RecordInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (!scope.IsDefined<TypeInfo>(Type) || !(scope.GetItem<TypeInfo>(Type) is RecordInfo info))
                errors.Add(new SemanticError
                {
                    Message = $"Cannot instantiate the undefined record type '{Type}'",
                    Node = this
                });
            else
            {
                RecordInfo = info;

                if (Children.Count - 1 != info.FieldNames.Length)
                    errors.Add(new SemanticError
                    {
                        Message = $"Invalid number of fields in record literal",
                        Node = this
                    });
                else
                    for (int i = 1; i < Children.Count; i++)
                        if (Children[i].Type != Types.Nil && !scope.SameType(Children[i].Type, info.FieldTypes[i - 1]))
                            errors.Add(new SemanticError
                            {
                                Message = $"Expression of type '{Children[i].Type}' cannot be assigned to field " +
                                          $"'{info.FieldNames[i - 1]}' with type '{info.FieldTypes[i - 1]}'",
                                Node = Children[i]
                            });
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            string type = RecordInfo.Name;

            ILGenerator il = generator.Generator;
            LocalBuilder record = il.DeclareLocal(generator.Types[type]);

            il.Emit(OpCodes.Newobj, generator.Types[type].GetConstructor(new Type[] { }));
            il.Emit(OpCodes.Stloc, record);
            il.Emit(OpCodes.Ldloc, record);

            for (int i = 1; i < Children.Count; i++)
            {
                var field = (FieldNode)Children[i];
                il.Emit(OpCodes.Dup);
                field.Generate(generator);
                il.Emit(OpCodes.Stfld, generator.Fields[type][field.Name]);
            }
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ldloc, record);
        }
    }
}

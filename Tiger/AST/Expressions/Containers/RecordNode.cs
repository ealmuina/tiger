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
            get { return (Children[0] as IdNode).Name; }
        }

        public RecordInfo RecordInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (!scope.IsDefined<TypeInfo>(Type) || !(scope.GetItem<TypeInfo>(Type) is RecordInfo))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Cannot instantiate the undefined record type '{0}'", Type),
                    Node = this
                });
            else
            {
                var info = scope.GetItem<TypeInfo>(Type);
                RecordInfo = (RecordInfo)info;

                if (Children.Count - 1 != RecordInfo.FieldNames.Length)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Invalid number of fields in record literal"),
                        Node = this
                    });
                else
                    for (int i = 1; i < Children.Count; i++)
                        if (Children[i].Type != Types.Nil && !scope.SameType(Children[i].Type, RecordInfo.FieldTypes[i - 1]))
                            errors.Add(new SemanticError
                            {
                                Message = string.Format("Expression of type '{0}' cannot be assigned to field '{1}' with type '{2}'",
                                                        Children[i].Type, RecordInfo.FieldNames[i - 1], RecordInfo.FieldTypes[i - 1]),
                                Node = Children[i]
                            });
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            LocalBuilder record = il.DeclareLocal(generator.Types[RecordInfo.Name]);

            il.Emit(OpCodes.Newobj, generator.Types[Type].GetConstructor(new Type[] { }));
            il.Emit(OpCodes.Stloc, record);
            il.Emit(OpCodes.Ldloc, record);

            for (int i = 1; i < Children.Count; i++)
            {
                var field = (FieldNode)Children[i];
                il.Emit(OpCodes.Dup);
                field.Generate(generator);
                il.Emit(OpCodes.Stfld, generator.Fields[Type][field.Name]);
            }
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ldloc, record);
        }
    }
}

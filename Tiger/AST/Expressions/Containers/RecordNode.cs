using System;
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
    class RecordNode : ExpressionNode
    {
        public RecordNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return (Children[0] as IdNode).Name; }
        }

        public TypeInfo RecordType { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (!scope.IsDefined<TypeInfo>(Type))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Cannot instantiate the undefined type '{0}'", Type),
                    Node = this
                });
            else
            {
                TypeInfo info = scope.DefinedTypes[Type];
                for (int i = 1; i < Children.Count; i++)
                    if (Children[i].Type != Types.Nil && !scope.SameType(Children[i].Type, info.FieldTypes[i - 1]))
                        errors.Add(new SemanticError
                        {
                            Message = string.Format("Expression of type '{0}' cannot be assigned to field '{1}' with type '{2}'",
                                                    Children[i].Type, info.FieldNames[i - 1], info.FieldTypes[i - 1]),
                            Node = Children[i]
                        });
                RecordType = info;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            LocalBuilder record = il.DeclareLocal(generator.Types[RecordType.Name]);

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

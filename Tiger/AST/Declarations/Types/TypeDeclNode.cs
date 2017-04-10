using System.Collections.Generic;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;

namespace Tiger.AST
{
    class TypeDeclNode : DeclarationNode
    {
        public TypeDeclNode(int line, int column) : base(line, column) { }

        public bool IsAlias
        {
            get => Children[1] is IdNode;
        }

        public bool IsArray
        {
            get => Children[1] is ArrayTypeNode;
        }

        public Semantics.TypeInfo TypeInfo { get; protected set; }

        public void DefineType(Scope scope, List<SemanticError> errors)
        {
            if (new string[] { Types.Int.Name, Types.String.Name }.Contains(Name))
                errors.Add(new SemanticError
                {
                    Message = $"Builtin type '{Name}' cannot be redefined",
                    Node = this
                });

            if (IsAlias)
            {
                var alias = (IdNode)Children[1];
                scope.DefineAliasType(Name, alias.Name);
            }
            else if (Children[1] is FieldsListNode record)
            {
                scope.DefineRecordType(Name, record.Names, record.TypesNames);
            }
            else //IsArray
            {
                var array = (ArrayTypeNode)Children[1];
                scope.DefineArrayType(Name, array.TypeName);
            }
        }

        public void CheckAlias(Scope scope, List<SemanticError> errors)
        {
            if ((IsAlias || IsArray) && scope.BadAlias(Name))
                errors.Add(new SemanticError
                {
                    Message = $"Type '{Name}' is part of an invalid alias cycle",
                    Node = this
                });
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));

            if (errors.Any()) return;

            TypeInfo = scope.GetItem<Semantics.TypeInfo>(Name);

            switch (TypeInfo)
            {
                case ArrayInfo aInfo:
                    aInfo.ElementsType = scope.GetItem<Semantics.TypeInfo>(aInfo.ElementsTypeName);
                    break;
                case RecordInfo rInfo:
                    rInfo.FieldTypes = rInfo.FieldTypesNames.Select(t => scope.GetItem<Semantics.TypeInfo>(t)).ToArray();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// If it's a record declaration, then define it with the generator
        /// </summary>
        public void Define(CodeGenerator generator)
        {
            if (Children[1] is FieldsListNode)
            {
                TypeBuilder typeBuilder = generator.Module.DefineType(Name + "_" + CodeGenerator.TypeId++, TypeAttributes.Public);
                typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
                generator.Types[TypeInfo] = typeBuilder;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            if (Children[1] is FieldsListNode record) // it's a record declaration. Define its fields!
            {
                var typeBuilder = (TypeBuilder)generator.Types[TypeInfo];
                var clone = new CodeGenerator(generator)
                {
                    Type = typeBuilder
                };
                Dictionary<string, FieldBuilder> fields = record.Define(clone);

                generator.Fields[(RecordInfo)TypeInfo] = fields;
                generator.Types[TypeInfo] = typeBuilder.CreateType();
            }
        }
    }
}

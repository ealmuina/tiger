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

        public Semantics.TypeInfo TypeInfo { get; protected set; }

        public void DefineType(Scope scope, List<SemanticError> errors)
        {
            if (IsAlias)
            {
                var alias = (IdNode)Children[1];
                scope.DefineAliasType(Name, alias.Name);
            }
            else if (Children[1] is FieldsListNode record)
            {
                scope.DefineRecordType(Name, record.Names, record.Types);
            }
            else //Children[1] is ArrayTypeNode
            {
                var array = (ArrayTypeNode)Children[1];
                scope.DefineArrayType(Name, array.Type);
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));

            if (new string[] { Types.Int, Types.String }.Contains(Name))
                errors.Add(new SemanticError
                {
                    Message = $"Builtin type '{Name}' cannot be redefined",
                    Node = this
                });

            if ((IsAlias || Children[1] is ArrayTypeNode) && scope.BadAlias(Name))
                errors.Add(new SemanticError
                {
                    Message = $"Type '{Name}' is part of an invalid alias cycle",
                    Node = this
                });
            else
                TypeInfo = scope.GetItem<Semantics.TypeInfo>(Name);
        }

        /// <summary>
        /// If it's record declaration, then defines it with the generator
        /// </summary>
        public void Define(CodeGenerator generator)
        {
            if (Children[1] is FieldsListNode)
            {
                TypeBuilder typeBuilder = generator.Module.DefineType(Name + "_" + CodeGenerator.TypeId++, TypeAttributes.Public);
                typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
                generator.Types[Name] = typeBuilder;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            if (Children[1] is FieldsListNode record) // it's a record declaration. Define its fields!
            {       
                var typeBuilder = (TypeBuilder)generator.Types[Name];
                var clone = new CodeGenerator(generator)
                {
                    Type = typeBuilder
                };
                Dictionary<string, FieldBuilder> fields = record.Define(clone);

                generator.Fields[Name] = fields;
                generator.Types[Name] = typeBuilder.CreateType();
            }
        }
    }
}

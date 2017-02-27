using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;

namespace Tiger.AST
{
    class TypeDeclNode : DeclarationNode
    {
        public TypeDeclNode(int line, int column) : base(line, column) { }

        public bool IsAlias
        {
            get { return Children[1] is IdNode; }
        }

        public Semantics.TypeInfo TypeInfo { get; protected set; }

        public void DefineType(Scope scope, List<SemanticError> errors)
        {
            if (IsAlias)
            {
                var alias = (IdNode)Children[1];
                scope.DefineType(Name, alias.Name);
            }
            else if (Children[1] is RecordTypeNode)
            {
                var record = (RecordTypeNode)Children[1];
                scope.DefineType(Name, record.Names, record.Types);
            }
            else //Children[1] is ArrayTypeNode
            {
                var array = (ArrayTypeNode)Children[1];
                scope.DefineType(Name, new string[] { "Array" }, new string[] { array.Type }, true);
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (IsAlias && scope.BadAlias(Name))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Type '{0}' is part of an invalid alias cycle", Name),
                    Node = this
                });
            else
                TypeInfo = scope.GetItem<Semantics.TypeInfo>(Name);
        }

        public void Define(CodeGenerator generator)
        {
            if (Children[1] is RecordTypeNode)
            {
                TypeBuilder typeBuilder = generator.Module.DefineType(Name, TypeAttributes.Public);
                typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
                generator.Types[Name] = typeBuilder;
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            if (Children[1] is RecordTypeNode)
            {
                var typeBuilder = (TypeBuilder)generator.Types[Name];

                var clone = new CodeGenerator(generator);
                clone.Type = typeBuilder;
                Dictionary<string, FieldBuilder> fields = (Children[1] as RecordTypeNode).Define(clone);
                generator.Fields[Name] = fields;

                generator.Types[Name] = typeBuilder.CreateType();
            }
        }
    }
}

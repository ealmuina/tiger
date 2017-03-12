using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System;

namespace Tiger.AST
{
    class TypeDeclListNode : Node, IDeclarationList
    {
        public string[] DeclaredNames
        {
            get
            {
                return (from f in Children.Cast<TypeDeclNode>()
                        select f.Name).ToArray();
            }
        }

        public TypeDeclListNode(ParserRuleContext context) : base(context) { }

        private void FixArrayType(TypeDeclNode type, CodeGenerator generator)
        {
            var info = (ArrayInfo)type.TypeInfo;
            generator.Types[type.Name] = generator.Types.ContainsKey(info.ElementsType) ?
                generator.Types[info.ElementsType].MakeArrayType() : null;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var types = Children.Cast<TypeDeclNode>();

            foreach (var type in types)
                type.DefineType(scope, errors);

            foreach (var type in types)
                type.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var type in Children.Cast<TypeDeclNode>())
            {
                type.Define(generator);
                if (type.TypeInfo is ArrayInfo)
                    FixArrayType(type, generator);
            }

            //Store real name for aliases
            foreach (var type in Children.Cast<TypeDeclNode>().Where(t => !(t.TypeInfo is ArrayInfo)))
                generator.Types[type.Name] = generator.Types[type.TypeInfo.Name];

            while (generator.Types.ContainsValue(null))
                foreach (var type in Children.Cast<TypeDeclNode>().Where(t => t.TypeInfo is ArrayInfo))
                    FixArrayType(type, generator);

            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

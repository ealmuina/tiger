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
        public TypeDeclListNode(ParserRuleContext context) : base(context) { }

        public string[] DeclaredNames
        {
            get => (from f in Children.Cast<TypeDeclNode>() select f.Name).ToArray();
        }

        /// <summary>
        /// Register in the generator a Type that corresponds with the specified array type if its elements type is already in the generator
        /// </summary>
        void FixArrayType(TypeDeclNode type, CodeGenerator generator)
        {
            var info = (ArrayInfo)type.TypeInfo;
            generator.Types[type.Name] = generator.Types.ContainsKey(info.ElementsType) ?
                generator.Types[info.ElementsType].MakeArrayType() : null;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var types = Children.Cast<TypeDeclNode>().ToList();

            types.ForEach(t => t.DefineType(scope, errors));
            types.ForEach(t => t.CheckSemantics(scope, errors));
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var type in Children.Cast<TypeDeclNode>())
            {
                type.Define(generator);
                if (type.TypeInfo is ArrayInfo)
                    FixArrayType(type, generator);
            }

            //Store real type for aliases
            foreach (var type in Children.Cast<TypeDeclNode>().Where(t => !(t.TypeInfo is ArrayInfo)))
                generator.Types[type.Name] = generator.Types[type.TypeInfo.Name];

            // Fix arrays
            while (generator.Types.ContainsValue(null))
                foreach (var type in Children.Cast<TypeDeclNode>().Where(t => t.TypeInfo is ArrayInfo))
                    FixArrayType(type, generator);

            Children.ForEach(n => n.Generate(generator));
        }
    }
}

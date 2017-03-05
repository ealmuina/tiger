using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class TypeDeclListNode : Node
    {
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
            {
                //if (types.Count(t => t.Name == type.Name) > 1)
                if (scope.IsDefined<TypeInfo>(type.Name))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Type '{0}' is already declared", type.Name),
                        Node = this
                    });

                type.DefineType(scope, errors);
            }

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
                else
                    generator.Types[type.Name] = generator.Types[type.TypeInfo.Name]; //Store real name for aliases
            }

            while (generator.Types.ContainsValue(null))
                foreach (var type in Children.Cast<TypeDeclNode>().Where(t => t.TypeInfo is ArrayInfo))
                    FixArrayType(type, generator);

            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

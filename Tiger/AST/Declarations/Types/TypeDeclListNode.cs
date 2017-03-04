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

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var types = Children.Cast<TypeDeclNode>();

            foreach (var type in types)
            {
                if (types.Count(t => t.Name == type.Name) > 1)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Type '{0}' is declared in a types declaration sequence several times", type.Name),
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
                generator.Types[type.Name] = generator.Types[type.TypeInfo.Name]; //Store real name for aliases
            }

            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

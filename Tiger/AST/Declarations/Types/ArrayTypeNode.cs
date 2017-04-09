using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class ArrayTypeNode : TypeNode
    {
        public ArrayTypeNode(ParserRuleContext context) : base(context) { }

        public string TypeName
        {
            get => (Children[0] as IdNode).Name;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (!scope.IsDefined<TypeInfo>(TypeName))
                errors.Add(new SemanticError
                {
                    Message = $"Unknown type '{TypeName}' on array declaration",
                    Node = this
                });

            Children.ForEach(n => n.CheckSemantics(scope, errors));
            if (errors.Count > 0) return;

            Type = scope.GetItem<TypeInfo>(TypeName);
        }

        public override void Generate(CodeGenerator generator)
        {
            //pass
        }
    }
}

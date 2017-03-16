using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class ArrayTypeNode : TypeNode
    {
        public ArrayTypeNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return (Children[0] as IdNode).Name; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (!scope.Types.ContainsKey(Type))
                errors.Add(new SemanticError
                {
                    Message = $"Unknown type '{Type}' on array declaration",
                    Node = this
                });

            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            //pass
        }
    }
}

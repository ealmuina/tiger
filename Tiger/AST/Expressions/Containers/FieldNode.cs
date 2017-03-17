using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class FieldNode : Node
    {
        public FieldNode(ParserRuleContext context) : base(context) { }

        public FieldNode(int line, int column) : base(line, column) { }

        public string Name => (Children[0] as IdNode).Name;

        public override string Type => Children[1].Type;

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            Children[1].Generate(generator);
        }
    }
}

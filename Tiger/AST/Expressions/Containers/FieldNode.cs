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

        public string Name
        {
            get => (Children[0] as IdNode).Name;
        }

        public ExpressionNode Expression
        {
            get => Children[1] as ExpressionNode;
        }

        public override string Type
        {
            get => Children[1].Type;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));
        }

        public override void Generate(CodeGenerator generator)
        {
            Expression.Generate(generator);
        }
    }
}

using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class ExpressionNode : Node
    {
        public ExpressionNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode(int line, int column): base(line, column) { }
    }
}

using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class UnaryNode : ExpressionNode
    {
        public UnaryNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Operand => Children[0] as ExpressionNode;
    }
}

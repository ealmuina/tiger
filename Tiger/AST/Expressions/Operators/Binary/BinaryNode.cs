using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class BinaryNode : ExpressionNode
    {
        public BinaryNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode LeftOperand => Children[0] as ExpressionNode;

        public ExpressionNode RightOperand => Children[1] as ExpressionNode;
    }
}

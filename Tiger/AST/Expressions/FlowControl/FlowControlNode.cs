using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class FlowControlNode : ExpressionNode
    {
        public FlowControlNode(ParserRuleContext context) : base(context) { }
    }
}

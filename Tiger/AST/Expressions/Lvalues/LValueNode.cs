using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class LValueNode : ExpressionNode
    {
        public LValueNode(ParserRuleContext context) : base(context) { }

        public LValueNode(int line, int column) : base(line, column) { }

        public bool ByValue { get; set; }
    }
}

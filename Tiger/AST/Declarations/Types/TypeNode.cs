using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class TypeNode : Node
    {
        public TypeNode(ParserRuleContext context) : base(context) { }

        public TypeNode(int line, int column) : base(line, column) { }
    }
}

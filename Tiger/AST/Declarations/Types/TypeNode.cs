using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class TypeNode : Node
    {
        public TypeNode(ParserRuleContext context) : base(context) { }
    }
}

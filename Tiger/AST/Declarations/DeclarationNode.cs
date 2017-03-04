using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class DeclarationNode : Node
    {
        public DeclarationNode(ParserRuleContext context) : base(context) { }

        public DeclarationNode(int line, int column): base(line, column) { }

        public string Name
        {
            get { return (Children[0] as IdNode).Name; }
        }
    }
}

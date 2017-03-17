using Antlr4.Runtime;

namespace Tiger.AST
{
    class IdNode : AuxiliaryNode
    {
        public IdNode(ParserRuleContext context, string name) : base(context)
        {
            Name = name;
        }

        public IdNode(int line, int column, string name) : base(line, column)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

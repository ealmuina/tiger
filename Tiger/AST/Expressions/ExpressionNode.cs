using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class ExpressionNode : Node
    {
        public ExpressionNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode(int line, int column): base(line, column) { }
    }
}

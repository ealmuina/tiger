using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    abstract class FlowControlNode : ExpressionNode
    {
        public FlowControlNode(ParserRuleContext context) : base(context) { }
    }
}

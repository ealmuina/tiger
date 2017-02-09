using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class LValueNode : ExpressionNode
    {
        public LValueNode(ParserRuleContext context) : base(context) { }

        public LValueNode(int line, int column) : base(line, column) { }
    }
}

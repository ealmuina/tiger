using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.AST
{
    abstract class LogicalNode : BinaryNode
    {
        public LogicalNode(ParserRuleContext context) : base(context) { }
    }
}

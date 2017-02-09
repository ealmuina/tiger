using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    abstract class AuxiliaryNode : Node
    {
        public AuxiliaryNode(ParserRuleContext context) : base(context) { }

        public AuxiliaryNode(int line, int column) : base(line, column) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    abstract class ProgramNode : Node
    {
        public ExpressionNode Expression { get; protected set; }
    }
}

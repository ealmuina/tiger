using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    abstract class TypeNode : Node
    {
        public TypeNode(ParserRuleContext context) : base(context) { }

        public TypeNode(int line, int column) : base(line, column) { }
    }
}

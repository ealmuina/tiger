using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    abstract class ComparisonNode : BinaryNode
    {
        public ComparisonNode(ParserRuleContext context) : base(context) { }
    }
}

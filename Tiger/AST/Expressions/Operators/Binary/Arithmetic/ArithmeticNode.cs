using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class ArithmeticNode : BinaryNode
    {
        public ArithmeticNode(ParserRuleContext context) : base(context) { }

        protected override string OperationType
        {
            get { return "arithmetic"; }
        }
    }
}

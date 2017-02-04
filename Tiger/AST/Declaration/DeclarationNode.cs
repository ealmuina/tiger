using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGen;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class DeclarationNode : Node
    {
        public DeclarationNode(ParserRuleContext context) : base(context) { }

        public DeclarationNode(int line, int column): base(line, column) { }
    }
}

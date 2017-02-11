using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class AtomNode : ExpressionNode
    {
        public AtomNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }
    }
}

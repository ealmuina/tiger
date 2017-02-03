using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class UnaryNode : ExpressionNode
    {
        public ExpressionNode Operand
        {
            get { return Children[0] as ExpressionNode; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Operand.CheckSemantics(scope, errors);
        }
    }
}

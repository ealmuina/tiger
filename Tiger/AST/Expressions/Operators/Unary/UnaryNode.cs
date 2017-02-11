using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class UnaryNode : ExpressionNode
    {
        public UnaryNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Operand
        {
            get { return Children[0] as ExpressionNode; }
        }

        public override string Type
        {
            get
            {
                return Children[0].Type;
            }

            protected set
            {
                base.Type = value;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Operand.CheckSemantics(scope, errors);
        }
    }
}

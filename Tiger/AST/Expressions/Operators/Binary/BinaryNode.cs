using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class BinaryNode : ExpressionNode
    {
        public BinaryNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode LeftOperand
        {
            get { return Children[0] as ExpressionNode; }
        }

        public ExpressionNode RightOperand
        {
            get { return Children[1] as ExpressionNode; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);
        }
    }
}

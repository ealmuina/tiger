using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semantics;

namespace Tiger.AST
{
    // TODO Pending
    class AssignNode : ExpressionNode
    {
        public LValueNode LValue
        {
            get { return Children[0] as LValueNode; }
        }

        public ExpressionNode Expression
        {
            get { return Children[1] as ExpressionNode; }
        }

        public VariableInfo SymbolInfo { get; private set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            throw new NotImplementedException();
        }

        public override void Generate(ILGenerator generator)
        {
            throw new NotImplementedException();
        }
    }
}

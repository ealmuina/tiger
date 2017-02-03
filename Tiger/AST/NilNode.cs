using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NilNode : ExpressionNode
    {
        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            // pass
        }

        public override void Generate(ILGenerator generator)
        {
            // pass
        }
    }
}

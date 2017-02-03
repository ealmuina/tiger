using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semantics;

namespace Tiger.AST
{
    class IntegerNode : ExpressionNode
    {
        public int? Value
        {
            get
            {
                int value;
                return int.TryParse(Text, out value) ? (int?)value : null;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (Value == null)
                errors.Add(SemanticError.InvalidNumber(Text, this));
        }

        public override void Generate(ILGenerator generator)
        {
            generator.Emit(OpCodes.Ldc_I4, (int)Value);
        }
    }
}

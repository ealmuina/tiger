using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    class NegativeNode : UnaryNode
    {
        public override void Generate(ILGenerator generator)
        {
            Operand.Generate(generator);
            generator.Emit(OpCodes.Neg);
        }
    }
}

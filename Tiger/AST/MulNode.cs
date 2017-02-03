using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.AST
{
    class MulNode : BinaryNode
    {
        public override OpCode OperatorOpCode
        {
            get { return OpCodes.Mul; }
        }
    }
}

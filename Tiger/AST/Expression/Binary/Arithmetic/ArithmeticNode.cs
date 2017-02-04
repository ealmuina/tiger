using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGen;

namespace Tiger.AST
{
    abstract class ArithmeticNode : BinaryNode
    {
        public ArithmeticNode(ParserRuleContext context) : base(context) { }

        public abstract OpCode OperatorOpCode { get; }

        public override void Generate(CodeGenerator generator)
        {
            LeftOperand.Generate(generator);
            RightOperand.Generate(generator);
            generator.Emit(OperatorOpCode);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class ArithmeticNode : BinaryNode
    {
        public ArithmeticNode(ParserRuleContext context) : base(context) { }

        public abstract OpCode OperatorOpCode { get; }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);
            RightOperand.Generate(generator, symbols);
            generator.Generator.Emit(OperatorOpCode);
        }
    }
}

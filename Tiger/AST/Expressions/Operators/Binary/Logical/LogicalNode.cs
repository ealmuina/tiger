using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using System.Reflection.Emit;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    abstract class LogicalNode : BinaryNode
    {
        public LogicalNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return "Int"; }
        }

        public abstract OpCode OperatorOpCode { get; }

        public abstract OpCode ShortCircuitOpCode { get; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (LeftOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    "binary logical", LeftOperand.Type == "Nil" ? "valued" : "integer", "left", LeftOperand));

            if (RightOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    "binary logical", RightOperand.Type == "Nil" ? "valued" : "integer", "right", RightOperand));
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);

            ILGenerator il = generator.Generator;
            Label end = il.DefineLabel();

            // Put 1 on evaluation stack if first operand is not 0; 0 otherwise
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Cgt);

            // If first operand matches short circuit condition (& -> 0 (false), | -> 1 (true)) then return 
            il.Emit(OpCodes.Dup);
            il.Emit(ShortCircuitOpCode);
            il.Emit(OpCodes.Beq, end);

            RightOperand.Generate(generator, symbols);

            // Put 1 on evaluation stack if second operand is not 0; 0 otherwise
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Cgt);

            // Perform bitwise operation
            il.Emit(OperatorOpCode);
            il.MarkLabel(end);
        }
    }
}

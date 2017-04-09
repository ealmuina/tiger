using System.Collections.Generic;
using Antlr4.Runtime;
using Tiger.Semantics;
using System.Reflection.Emit;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    abstract class LogicalNode : BinaryNode
    {
        public LogicalNode(ParserRuleContext context) : base(context)
        {
            Type = Types.Int;
        }

        public abstract OpCode OperatorOpCode { get; }

        public abstract OpCode ShortCircuitOpCode { get; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (LeftOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    "binary logical", LeftOperand.Type.Equals(Types.Nil) ? "valued" : "integer", "left", LeftOperand));

            if (RightOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    "binary logical", RightOperand.Type.Equals(Types.Nil) ? "valued" : "integer", "right", RightOperand));
        }

        public override void Generate(CodeGenerator generator)
        {
            LeftOperand.Generate(generator);

            ILGenerator il = generator.Generator;
            Label end = il.DefineLabel();

            // Put 1 on evaluation stack if first operand is not 0; 0 otherwise
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);

            // If first operand matches short circuit condition (& -> 0 (false), | -> 1 (true)) then return 
            il.Emit(OpCodes.Dup);
            il.Emit(ShortCircuitOpCode);
            il.Emit(OpCodes.Beq, end);

            RightOperand.Generate(generator);

            // Put 1 on evaluation stack if second operand is not 0; 0 otherwise
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);

            // Perform bitwise operation
            il.Emit(OperatorOpCode);
            il.MarkLabel(end);
        }
    }
}

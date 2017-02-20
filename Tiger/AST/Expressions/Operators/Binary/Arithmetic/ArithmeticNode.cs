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

        public abstract string OperatorName { get; }

        public override string Type
        {
            get { return Types.Int; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (errors.Count > 0)
                return;

            if (LeftOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    OperatorName, LeftOperand.Type == Types.Nil ? "valued" : "integer", "left", LeftOperand));

            if (RightOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    OperatorName, RightOperand.Type == Types.Nil ? "valued" : "integer", "right", RightOperand));
        }

        public override void Generate(CodeGenerator generator)
        {
            LeftOperand.Generate(generator);
            RightOperand.Generate(generator);
            generator.Generator.Emit(OperatorOpCode);
        }
    }
}

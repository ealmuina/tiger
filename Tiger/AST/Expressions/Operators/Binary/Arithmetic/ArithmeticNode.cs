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
            get { return "Int"; }
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);
            RightOperand.Generate(generator, symbols);
            generator.Generator.Emit(OperatorOpCode);
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (LeftOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    OperatorName, LeftOperand.Type == "Nil" ? "valued" : "integer", "left", LeftOperand));

            if (RightOperand.Type != Type)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    OperatorName, RightOperand.Type == "Nil" ? "valued" : "integer", "right", RightOperand));
        }
    }
}

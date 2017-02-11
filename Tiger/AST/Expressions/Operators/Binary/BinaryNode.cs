using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class BinaryNode : ExpressionNode
    {
        public BinaryNode(ParserRuleContext context) : base(context) { }

        public abstract OpCode OperatorOpCode { get; }

        public ExpressionNode LeftOperand
        {
            get { return Children[0] as ExpressionNode; }
        }

        public ExpressionNode RightOperand
        {
            get { return Children[1] as ExpressionNode; }
        }

        public override string Type
        {
            get { return "Int"; }
        }

        protected abstract string OperationType { get; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (LeftOperand.Type != "Int")
                errors.Add(SemanticError.InvalidBinaryOperation(OperationType, "left", LeftOperand));

            if (RightOperand.Type != "Int")
                errors.Add(SemanticError.InvalidBinaryOperation(OperationType, "right", RightOperand));
    }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            LeftOperand.Generate(generator, symbols);
            RightOperand.Generate(generator, symbols);
            generator.Generator.Emit(OperatorOpCode);
        }
    }
}

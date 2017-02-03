using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class BinaryNode : ExpressionNode
    {
        public ExpressionNode LeftOperand
        {
            get { return Children[0] as ExpressionNode; }
        }

        public ExpressionNode RightOperand
        {
            get { return Children[1] as ExpressionNode; }
        }

        public abstract OpCode OperatorOpCode { get; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);
        }

        public override void Generate(ILGenerator generator)
        {
            LeftOperand.Generate(generator);
            RightOperand.Generate(generator);
            generator.Emit(OperatorOpCode);
        }
    }
}

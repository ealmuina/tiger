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
    class NegativeNode : UnaryNode
    {
        public NegativeNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return Types.Int; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (Operand.Type != Types.Int)
                errors.Add(SemanticError.InvalidUseOfOperator(
                    "unary minus", Operand.Type == Types.Nil ? "valued" : "integer", "operand", Operand));

            Operand.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            Operand.Generate(generator);
            generator.Generator.Emit(OpCodes.Neg);
        }
    }
}

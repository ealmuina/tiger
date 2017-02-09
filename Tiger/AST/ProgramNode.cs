using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class ProgramNode : Node
    {
        public ProgramNode(ParserRuleContext context, ExpressionNode expression) : base(context)
        {
            Expression = expression;
        }

        public ExpressionNode Expression { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Expression.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            Expression.Generate(generator);
            generator.Generator.Emit(OpCodes.Ret);
        }
    }
}

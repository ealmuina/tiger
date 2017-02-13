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
        public ProgramNode(ParserRuleContext context) : base(context) { }

        public ExpressionNode Expression { get { return (ExpressionNode)Children[0]; } }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Expression.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            Expression.Generate(generator, symbols);
            if (Expression.Type != Types.Void)
                generator.Generator.Emit(OpCodes.Pop);
            generator.Generator.Emit(OpCodes.Ret);
        }
    }
}

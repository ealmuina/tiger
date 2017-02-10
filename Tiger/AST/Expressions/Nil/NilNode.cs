using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NilNode : ExpressionNode
    {
        public NilNode() : base(-1, -1) { }

        public NilNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            //pass
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class WhileNode : FlowControlNode
    {
        public WhileNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            throw new NotImplementedException();
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            throw new NotImplementedException();
        }
    }
}

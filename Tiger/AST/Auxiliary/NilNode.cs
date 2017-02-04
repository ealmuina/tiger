using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGen;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NilNode : AuxiliaryNode
    {
        public NilNode(ParserRuleContext context) : base(context) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            throw new NotImplementedException();
        }

        public override void Generate(CodeGenerator generator)
        {
            throw new NotImplementedException();
        }
    }
}

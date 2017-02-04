using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGen;
using Tiger.Semantics;

namespace Tiger.AST
{
    class VarDeclNode : DeclarationNode
    {
        public VarDeclNode(ParserRuleContext context) : base(context) { }

        public VarDeclNode(int line, int column) : base(line, column) { }

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

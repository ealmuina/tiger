using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGen;

namespace Tiger.AST
{
    class StringNode : AtomNode
    {
        public StringNode(ParserRuleContext context, string text) : base(context)
        {
            Text = text;
        }

        public string Text { get; protected set; }

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

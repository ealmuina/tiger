using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class DeclarationListNode : Node
    {
        public DeclarationListNode(ParserRuleContext context) : base(context) { }

        public DeclarationListNode(int line, int column) : base(line, column) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

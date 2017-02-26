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
    class LetNode : ExpressionNode
    {
        public LetNode(ParserRuleContext context) : base(context) { }

        public LetNode(int line, int column) : base(line, column) { }

        public override string Type
        {
            get
            {
                return Children.Count > 1 ?
                    Children.Last().Type : Types.Void;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            scope = (Scope)scope.Clone();
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            generator = (CodeGenerator)generator.Clone();
            foreach (var node in Children)
                node.Generate(generator);
        }
    }
}

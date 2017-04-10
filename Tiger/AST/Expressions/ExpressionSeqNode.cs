using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class ExpressionSeqNode : ExpressionNode
    {
        public ExpressionSeqNode(ParserRuleContext context) : base(context) { }

        public ExpressionSeqNode(int line, int column) : base(line, column) { }

        bool MayCauseBreak(Node node)
        {
            if (node == null || node is WhileNode || node is ForNode) return false;
            return node is BreakNode || node.Children.Exists(n => MayCauseBreak(n));
        }

        bool InLoop { get; set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            InLoop = scope.InsideLoop;

            foreach (var expr in Children)
            {
                expr.CheckSemantics(scope, errors);
                if (errors.Any()) return;
            }

            // Set return type according with Clarification/Modification 8
            if (Children.Any() && !(InLoop && MayCauseBreak(this)))
                Type = Children.Last().Type;
            else
                Type = Types.Void;
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var expr in Children)
            {
                expr.Generate(generator);

                if (expr.Type != Types.Void && (expr != Children.Last() || (expr == Children.Last() && Type == Types.Void)))
                    generator.Generator.Emit(OpCodes.Pop);
            }
        }
    }
}

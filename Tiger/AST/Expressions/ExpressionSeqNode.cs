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
    class ExpressionSeqNode : ExpressionNode
    {
        public ExpressionSeqNode(ParserRuleContext context) : base(context) { }

        public ExpressionSeqNode(int line, int column) : base(line, column) { }

        bool MayCauseBreak(Node node)
        {
            return node != null && (node is BreakNode || node.Children.Exists(n => MayCauseBreak(n)));
        }

        bool InLoop { get; set; }

        public override string Type
        {
            get
            {
                if (Children.Count > 0 && !(InLoop && MayCauseBreak(this)))
                    return Children.Last().Type;
                return Types.Void;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            InLoop = scope.InsideLoop;

            foreach (var expr in Children)
            {
                int errorsCount = errors.Count;
                expr.CheckSemantics(scope, errors);

                if (errorsCount == errors.Count && !scope.Types.ContainsKey(expr.Type))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Type '{0}' returned by the expression isn't visible in its context", expr.Type),
                        Node = expr
                    });
            }
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

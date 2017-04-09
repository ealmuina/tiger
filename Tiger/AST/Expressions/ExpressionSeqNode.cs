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

                if (errors.Count > 0) return;

                //if (!scope.IsDefined<TypeInfo>(expr.Type.Name) || scope.GetItem<TypeInfo>(expr.Type.Name) != expr.Type)
                //    errors.Add(new SemanticError
                //    {
                //        Message = $"Type '{expr.Type}' returned by the expression isn't visible in its context",
                //        Node = expr
                //    });
            }

            if (Children.Count > 0 && !(InLoop && MayCauseBreak(this)))
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

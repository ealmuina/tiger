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

        bool MayCauseBreak(Node node)
        {
            return node is BreakNode || node.Children.Exists(n => MayCauseBreak(n));
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
                expr.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator)
        {
            generator.Generator.Emit(OpCodes.Nop);
            foreach (var expr in Children)
            {
                expr.Generate(generator);
                if (expr != Children.Last() && expr.Type != Types.Void)
                    generator.Generator.Emit(OpCodes.Pop);
            }
        }
    }
}

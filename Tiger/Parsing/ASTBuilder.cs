using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Tiger.AST;

namespace Tiger.Parsing
{
    class ASTBuilder : TigerBaseVisitor<Node>
    {
        #region Unary
        public override Node VisitString([NotNull] TigerParser.StringContext context)
        {
            return new StringNode(context, context.STRING().GetText());
        }

        public override Node VisitInteger([NotNull] TigerParser.IntegerContext context)
        {
            return new IntegerNode(context, context.INTEGER().GetText());
        }

        public override Node VisitNil([NotNull] TigerParser.NilContext context)
        {
            return new NilNode(context);
        }

        public override Node VisitLValue([NotNull] TigerParser.LValueContext context)
        {
            return Visit(context.lvalue());
        }

        public override Node VisitUnaryMinus([NotNull] TigerParser.UnaryMinusContext context)
        {
            var node = new NegativeNode(context);
            node.Children.Add(Visit(context.expr()));
            return node;
        }
        #endregion

        #region Binary
        public override Node VisitArithmetic([NotNull] TigerParser.ArithmeticContext context)
        {
            ArithmeticNode node;
            switch (context.op.Text)
            {
                case "*":
                    node = new MulNode(context);
                    break;
                case "/":
                    node = new DivNode(context);
                    break;
                case "+":
                    node = new PlusNode(context);
                    break;
                case "-":
                    node = new MinusNode(context);
                    break;
                default:
                    throw new NotSupportedException();
            }

            node.Children.Add(Visit(context.e1));
            node.Children.Add(Visit(context.e2));
            return node;
        }

        public override Node VisitComparison([NotNull] TigerParser.ComparisonContext context)
        {
            ComparisonNode node;
            switch (context.op.Text)
            {
                case "<>":
                    node = new NeNode(context);
                    break;
                case "=":
                    node = new EqNode(context);
                    break;
                case ">=":
                    node = new GeNode(context);
                    break;
                case "<=":
                    node = new LeNode(context);
                    break;
                case ">":
                    node = new GtNode(context);
                    break;
                case "<":
                    node = new LtNode(context);
                    break;
                default:
                    throw new NotSupportedException();
            }

            node.Children.Add(Visit(context.e1));
            node.Children.Add(Visit(context.e2));
            return node;
        }

        public override Node VisitLogical([NotNull] TigerParser.LogicalContext context)
        {
            LogicalNode node;
            switch (context.op.Text)
            {
                case "&":
                    node = new AndNode(context);
                    break;
                case "|":
                    node = new OrNode(context);
                    break;
                default:
                    throw new NotSupportedException();
            }

            node.Children.Add(Visit(context.e1));
            node.Children.Add(Visit(context.e2));
            return node;
        }
        #endregion

        public override Node VisitAssign([NotNull] TigerParser.AssignContext context)
        {
            var node = new AssignNode(context);
            node.Children.Add(Visit(context.lvalue()));
            node.Children.Add(Visit(context.expr()));
            return node;
        }

        public override Node VisitCall([NotNull] TigerParser.CallContext context)
        {
            var node = new FuncallNode(context);
            var id = new IdNode(context, context.ID().GetText());
            node.Children.Add(id);

            var args = from arg
                       in context.expr()
                       select Visit(arg);
            node.Children.AddRange(args);

            return node;
        }

        public override Node VisitParenExprs([NotNull] TigerParser.ParenExprsContext context)
        {
            return base.VisitParenExprs(context);   // TODO Implement
        }

        public override Node VisitRecord([NotNull] TigerParser.RecordContext context)
        {
            var node = new RecordNode(context);

            var typeId = new IdNode(context, context.typeID.Text);
            node.Children.Add(typeId);

            ITerminalNode[] ids = context.ID();
            TigerParser.ExprContext[] exprs = context.expr();

            for (int i = 0; i < ids.Length; i++)
            {
                var fieldId = new IdNode(
                    ids[i].Symbol.Line,
                    ids[i].Symbol.Column,
                    ids[i].GetText());
                        
                var fieldExpr = Visit(exprs[i]);

                var field = new FieldNode(fieldId.Line, fieldId.Column);
                field.Children.Add(fieldId);
                field.Children.Add(fieldExpr);

                node.Children.Add(field);
            }
            return node;
        }

        public override Node VisitArray([NotNull] TigerParser.ArrayContext context)
        {
            var node = new ArrayNode(context);
            var typeId = new IdNode(context, context.ID().GetText());

            node.Children.Add(typeId);
            node.Children.Add(Visit(context.e1));
            node.Children.Add(Visit(context.e2));

            return node;
        }

        #region Flow control
        public override Node VisitIf([NotNull] TigerParser.IfContext context)
        {
            var node = new IfNode(context);
            node.Children.Add(Visit(context.e1)); //If condition
            node.Children.Add(Visit(context.e2)); //Then expression
            node.Children.Add(Visit(context.e3)); //Else expression if any, oterwise null
            return node;
        }

        public override Node VisitWhile([NotNull] TigerParser.WhileContext context)
        {
            var node = new WhileNode(context);
            node.Children.Add(Visit(context.e1)); //Condition
            node.Children.Add(Visit(context.e2)); //Do expression
            return node;
        }

        public override Node VisitFor([NotNull] TigerParser.ForContext context)
        {
            var node = new ForNode(context);

            ITerminalNode id = context.ID();
            var init = new VarNode(id.Symbol.Line, id.Symbol.Column);
            init.Children.Add(new IdNode(id.Symbol.Line, id.Symbol.Column, id.GetText()));
            init.Children.Add(Visit(context.e1));

            node.Children.Add(init);
            node.Children.Add(Visit(context.e2)); //To expression
            node.Children.Add(Visit(context.e3)); //Do expression

            return node;
        }

        public override Node VisitBreak([NotNull] TigerParser.BreakContext context)
        {
            return new BreakNode(context);
        }
        #endregion

        public override Node VisitLet([NotNull] TigerParser.LetContext context)
        {
            var node = new LetNode(context);

            TigerParser.DeclContext[] decls = context.decl();
            var declarations = new DeclarationListNode(decls[0]);
            declarations.Children.AddRange(from decl in decls select Visit(decl));

            node.Children.Add(declarations);
            node.Children.AddRange(from expr in context.expr() select Visit(expr));

            return node;
        }
    }
}

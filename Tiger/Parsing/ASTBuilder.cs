using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Tiger.AST;
using Antlr4.Runtime;

namespace Tiger.Parsing
{
    class ASTBuilder : TigerBaseVisitor<Node>
    {
        public override Node VisitProgram([NotNull] TigerParser.ProgramContext context)
        {
            var node = new ProgramNode(context);
            node.Children.Add(Visit(context.expr()));
            return node;
        }

        #region Expressions
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

            node.Children.Add(Visit(context.expr(0)));
            node.Children.Add(Visit(context.expr(1)));
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

            node.Children.Add(Visit(context.expr(0)));
            node.Children.Add(Visit(context.expr(1)));
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

            node.Children.Add(Visit(context.expr(0)));
            node.Children.Add(Visit(context.expr(1)));
            return node;
        }

        public override Node VisitAssign([NotNull] TigerParser.AssignContext context)
        {
            var node = new AssignNode(context);
            node.Children.Add(Visit(context.lvalue()));
            node.Children.Add(Visit(context.expr()));
            return node;
        }
        #endregion

        #region Function Calls
        public override Node VisitCall([NotNull] TigerParser.CallContext context)
        {
            var node = new FuncallNode(context);
            var id = new IdNode(context, context.ID().GetText());
            node.Children.Add(id);
            node.Children.AddRange(from e in context.expr() select Visit(e));
            return node;
        }
        #endregion

        #region Let
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
        #endregion

        #region Containers
        public override Node VisitRecord([NotNull] TigerParser.RecordContext context)
        {
            var node = new RecordNode(context);

            var typeId = new IdNode(context, context.typeID.Text);
            node.Children.Add(typeId);

            ITerminalNode[] ids = context.ID();
            TigerParser.ExprContext[] exprs = context.expr();

            for (int i = 1; i < ids.Length; i++)
            {
                var fieldId = new IdNode(
                    ids[i].Symbol.Line,
                    ids[i].Symbol.Column,
                    ids[i].GetText());

                var fieldExpr = Visit(exprs[i - 1]);

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
        #endregion

        #region Flow control
        public override Node VisitIf([NotNull] TigerParser.IfContext context)
        {
            var node = new IfNode(context);
            node.Children.Add(Visit(context.e1)); //If condition
            node.Children.Add(Visit(context.e2)); //Then expression
            node.Children.Add(context.e3 == null ? new NilNode() : Visit(context.e3)); //Else expression if any, otherwise nil
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
            var init = new VarDeclNode(id.Symbol.Line, id.Symbol.Column);
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

        #region Lvalue
        public override Node VisitIdLValue([NotNull] TigerParser.IdLValueContext context)
        {
            return new IdNode(context, context.ID().GetText());
        }

        public override Node VisitFieldLValue([NotNull] TigerParser.FieldLValueContext context)
        {
            var node = new FieldAccessNode(context);
            node.Children.Add(Visit(context.lvalue()));

            ITerminalNode id = context.ID();
            node.Children.Add(
                new IdNode(
                id.Symbol.Line,
                id.Symbol.Column,
                id.GetText()));

            return node;
        }

        public override Node VisitIndexLValue([NotNull] TigerParser.IndexLValueContext context)
        {
            var node = new IndexNode(context);
            node.Children.Add(Visit(context.lvalue()));
            node.Children.Add(Visit(context.expr()));
            return node;
        }
        #endregion

        public override Node VisitParenExprs([NotNull] TigerParser.ParenExprsContext context)
        {
            var node = new ExpressionSeqNode(context);
            node.Children.AddRange(from e in context.expr() select Visit(e));
            return node;
        }
        #endregion

        #region Declarations
        #region Functions
        public override Node VisitFuncDecl([NotNull] TigerParser.FuncDeclContext context)
        {
            var node = new FuncDeclNode(context);

            IToken id = context.id;
            node.Children.Add(
                new IdNode(id.Line, id.Column, id.Text));
            node.Children.Add(
                context.type_fields() == null ?
                new NilNode() :
                Visit(context.type_fields()));

            IToken typeId = context.typeId;
            node.Children.Add(typeId == null ?
                new NilNode() as Node :
                new IdNode(
                    typeId.Line,
                    typeId.Column,
                    typeId.Text));
            node.Children.Add(Visit(context.expr()));

            return node;
        }
        #endregion

        #region Types
        public override Node VisitTypeDecl([NotNull] TigerParser.TypeDeclContext context)
        {
            var node = new TypeDeclNode(context);

            ITerminalNode id = context.ID();
            node.Children.Add(
                new IdNode(
                    id.Symbol.Line,
                    id.Symbol.Column,
                    id.GetText()));
            node.Children.Add(Visit(context.type()));

            return node;
        }

        public override Node VisitIdType([NotNull] TigerParser.IdTypeContext context)
        {
            return new IdNode(context, context.ID().GetText());
        }

        public override Node VisitRecordType([NotNull] TigerParser.RecordTypeContext context)
        {
            return Visit(context.type_fields());
        }

        public override Node VisitArrayType([NotNull] TigerParser.ArrayTypeContext context)
        {
            var node = new ArrayTypeNode(context);
            ITerminalNode id = context.ID();
            node.Children.Add(
                new IdNode(
                    id.Symbol.Line,
                    id.Symbol.Column,
                    id.GetText()));
            return node;
        }

        public override Node VisitTypeFields([NotNull] TigerParser.TypeFieldsContext context)
        {
            var node = new TypeFieldsNode(context);
            ITerminalNode[] ids = context.ID();

            for (int i = 0; i < ids.Length - 1; i += 2)
            {
                var name = new IdNode(
                    ids[i].Symbol.Line,
                    ids[i].Symbol.Column,
                    ids[i].GetText());
                var type = new IdNode(
                    ids[i + 1].Symbol.Line,
                    ids[i + 1].Symbol.Column,
                    ids[i + 1].GetText());

                var field = new TypeFieldNode(ids[i].Symbol.Line, ids[i].Symbol.Column);
                node.Children.Add(field);
            }

            return node;
        }
        #endregion

        #region Variables
        public override Node VisitVarDecl([NotNull] TigerParser.VarDeclContext context)
        {
            var node = new VarDeclNode(context);

            IToken id = context.id;
            node.Children.Add(
                new IdNode(
                    id.Line,
                    id.Column,
                    id.Text));

            IToken typeId = context.typeId;
            node.Children.Add(typeId == null ?
                new NilNode() as Node :
                new IdNode(
                    typeId.Line,
                    typeId.Column,
                    typeId.Text));

            node.Children.Add(Visit(context.expr()));
            return node;
        }
        #endregion
        #endregion
    }
}

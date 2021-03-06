﻿using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Tiger.AST;

namespace Tiger.Parsing
{
    class ASTBuilder : TigerBaseVisitor<Node>
    {
        public override Node VisitProgram([NotNull] TigerParser.ProgramContext context)
        {
            var node = new ProgramNode(context);
            node.Children.Add(Visit(context.expr())); // EXPRESSION
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
            var node = (LValueNode)Visit(context.lvalue());
            node.ByValue = true;
            return node;
        }

        public override Node VisitUnaryMinus([NotNull] TigerParser.UnaryMinusContext context)
        {
            var node = new NegativeNode(context);
            node.Children.Add(Visit(context.expr())); // OPERAND
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

            node.Children.Add(Visit(context.expr(0))); // LEFT EXPRESSION
            node.Children.Add(Visit(context.expr(1))); // RIGHT EXPRESSION
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

            node.Children.Add(Visit(context.expr(0)));  // LEFT EXPRESSION
            node.Children.Add(Visit(context.expr(1)));  // RIGHT EXPRESSION
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

            node.Children.Add(Visit(context.expr(0))); // LEFT EXPRESSION
            node.Children.Add(Visit(context.expr(1))); // RIGHT EXPRESSION
            return node;
        }

        public override Node VisitAssign([NotNull] TigerParser.AssignContext context)
        {
            var node = new AssignNode(context);

            var lvalue = (LValueNode)Visit(context.lvalue());
            lvalue.ByValue = false;
            node.Children.Add(lvalue); // LVALUE

            node.Children.Add(Visit(context.expr())); // EXPRESSION
            return node;
        }
        #endregion

        #region Function Calls
        public override Node VisitCall([NotNull] TigerParser.CallContext context)
        {
            var node = new FuncallNode(context);
            var id = new IdNode(context, context.ID().GetText());
            node.Children.Add(id); // FUNCTION NAME
            node.Children.AddRange(from e in context.expr() select Visit(e)); // PARAMETERS
            return node;
        }
        #endregion

        #region Let
        public override Node VisitLet([NotNull] TigerParser.LetContext context)
        {
            var node = new LetNode(context);

            TigerParser.DeclsContext[] decls = context.decls();
            var declarations = new DeclarationListNode(decls[0]);
            declarations.Children.AddRange(from d in decls select Visit(d)); // declaration list -> DECLARATION+
            node.Children.Add(declarations); // DECLARATION LIST

            TigerParser.ExprContext[] exprs = context.expr();

            ExpressionSeqNode expressions;
            if (exprs.Length > 0)
                expressions = new ExpressionSeqNode(exprs[0]);
            else
                expressions = new ExpressionSeqNode(-1, -1);

            expressions.Children.AddRange(from e in exprs select Visit(e)); // expression sequence -> EXPRESSION*
            node.Children.Add(expressions); // EXPRESSION SEQUENCE

            return node;
        }
        #endregion

        #region Containers
        public override Node VisitRecord([NotNull] TigerParser.RecordContext context)
        {
            var node = new RecordNode(context);

            var typeId = new IdNode(context, context.typeID.Text); 
            node.Children.Add(typeId); // RECORD TYPE

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
                field.Children.Add(fieldId); // field -> FIELD NAME
                field.Children.Add(fieldExpr); // field -> FIELD EXPRESSION

                node.Children.Add(field); // FIELD #i
            }
            return node;
        }

        public override Node VisitArray([NotNull] TigerParser.ArrayContext context)
        {
            var node = new ArrayNode(context);
            var typeId = new IdNode(context, context.ID().GetText());

            node.Children.Add(typeId); // TYPE
            node.Children.Add(Visit(context.expr(0))); // SIZE
            node.Children.Add(Visit(context.expr(1))); // INIT EXPRESSION

            return node;
        }
        #endregion

        #region Flow control
        public override Node VisitIf([NotNull] TigerParser.IfContext context)
        {
            var node = new IfNode(context);
            node.Children.Add(Visit(context.expr(0))); //IF EXPRESSION
            node.Children.Add(Visit(context.expr(1))); //THEN EXPRESSION

            var elseExpr = context.expr(2);
            node.Children.Add(elseExpr != null ? Visit(elseExpr) : null); //ELSE EXPRESSION if any, otherwise nil
            return node;
        }

        public override Node VisitWhile([NotNull] TigerParser.WhileContext context)
        {
            var node = new WhileNode(context);
            node.Children.Add(Visit(context.expr(0))); // CONDITION
            node.Children.Add(Visit(context.expr(1))); // DO EXPRESSION
            return node;
        }

        public override Node VisitFor([NotNull] TigerParser.ForContext context)
        {
            var node = new ForNode(context);

            ITerminalNode id = context.ID();
            var init = new VarDeclNode(id.Symbol.Line, id.Symbol.Column, true);
            init.Children.Add(new IdNode(id.Symbol.Line, id.Symbol.Column, id.GetText())); // init -> INIT VARIABLE NAME
            init.Children.Add(null); // init -> INIT VARIABLE TYPE
            init.Children.Add(Visit(context.expr(0))); // init -> INIT VARIABLE VALUE EXPRESSION

            node.Children.Add(init); // INIT VARIABLE
            node.Children.Add(Visit(context.expr(1))); //TO EXPRESSION
            node.Children.Add(Visit(context.expr(2))); //DO EXPRESSION

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
            return new VarAccessNode(context, context.ID().GetText());
        }

        public override Node VisitFieldLValue([NotNull] TigerParser.FieldLValueContext context)
        {
            var node = new FieldAccessNode(context);
            var lvalue = (LValueNode)Visit(context.lvalue());
            lvalue.ByValue = true;
            node.Children.Add(lvalue); // LVALUE

            ITerminalNode id = context.ID();
            node.Children.Add( // FIELD ID
                new IdNode(
                id.Symbol.Line,
                id.Symbol.Column,
                id.GetText()));

            return node;
        }

        public override Node VisitIndexLValue([NotNull] TigerParser.IndexLValueContext context)
        {
            var node = new IndexNode(context);

            var lvalue = (LValueNode)Visit(context.lvalue());
            lvalue.ByValue = true;
            node.Children.Add(lvalue); // LVALUE

            node.Children.Add(Visit(context.expr())); // INDEX EXPRESSION
            return node;
        }
        #endregion

        public override Node VisitParenExprs([NotNull] TigerParser.ParenExprsContext context)
        {
            var node = new ExpressionSeqNode(context);
            node.Children.AddRange(from e in context.expr() select Visit(e)); // EXPRESSION+
            return node;
        }
        #endregion

        #region Declarations

        #region Functions
        public override Node VisitFuncDecls([NotNull] TigerParser.FuncDeclsContext context)
        {
            var node = new FuncDeclListNode(context);
            node.Children.AddRange(from funcDecl in context.func_decl() select Visit(funcDecl)); // FUNCTIONS
            return node;
        }

        public override Node VisitFuncDecl([NotNull] TigerParser.FuncDeclContext context)
        {
            var node = new FuncDeclNode(context);

            // ID
            node.Children.Add(
                new IdNode(context.id.Line, context.id.Column, context.id.Text));

            // PARAMS ?
            TigerParser.Type_fieldsContext typeFields = context.type_fields();
            node.Children.Add(
                typeFields == null ?
                null :
                Visit(typeFields));

            // RETURN TYPE ?
            node.Children.Add(
                context.typeId == null ?
                null :
                new IdNode(context.typeId.Line, context.typeId.Column, context.typeId.Text));

            // BODY
            node.Children.Add(Visit(context.expr()));

            return node;
        }
        #endregion

        #region Types
        public override Node VisitTypeDecls([NotNull] TigerParser.TypeDeclsContext context)
        {
            var node = new TypeDeclListNode(context);
            for (int i = 0; i < context.type().Length; i++)
            {
                ITerminalNode id = context.ID(i);
                TigerParser.TypeContext type = context.type(i);

                var decl = new TypeDeclNode(id.Symbol.Line, id.Symbol.Column);
                decl.Children.Add(
                    new IdNode(id.Symbol.Line, id.Symbol.Column, id.GetText()));
                decl.Children.Add(
                    Visit(type));

                node.Children.Add(decl);
            }
            return node;
        }

        public override Node VisitIdType([NotNull] TigerParser.IdTypeContext context)
        {
            return new IdNode(context, context.ID().GetText());
        }

        public override Node VisitRecordType([NotNull] TigerParser.RecordTypeContext context)
        {
            try
            {
                return Visit(context.type_fields());
            }
            catch (Exception)
            {
                return new FieldsListNode(context, new string[] { }, new string[] { });
            }
        }

        public override Node VisitArrayType([NotNull] TigerParser.ArrayTypeContext context)
        {
            var node = new ArrayTypeNode(context);
            ITerminalNode id = context.ID();
            node.Children.Add( // ELEMENTS TYPE
                new IdNode(
                    id.Symbol.Line,
                    id.Symbol.Column,
                    id.GetText()));
            return node;
        }

        public override Node VisitTypeFields([NotNull] TigerParser.TypeFieldsContext context)
        {
            var names = new List<string>();
            var types = new List<string>();

            ITerminalNode[] ids = context.ID();

            for (int i = 0; i < ids.Length; i++)
            {
                if (i % 2 == 0)
                    names.Add(ids[i].GetText()); // names are in even positions
                else
                    types.Add(ids[i].GetText()); //types are in odd positions
            }

            var node = new FieldsListNode(context, names.ToArray(), types.ToArray());
            return node;
        }
        #endregion

        #region Variables
        public override Node VisitVarDecl([NotNull] TigerParser.VarDeclContext context)
        {
            var node = new VarDeclNode(context);

            node.Children.Add( // NAME
                new IdNode(
                    context.id.Line,
                    context.id.Column,
                    context.id.Text));

            node.Children.Add(context.typeId == null ? // TYPE
                null :
                new IdNode(
                    context.typeId.Line,
                    context.typeId.Column,
                    context.typeId.Text));

            node.Children.Add(Visit(context.expr())); // EXPRESSION
            return node;
        }
        #endregion
        #endregion
    }
}

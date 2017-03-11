//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\Zchool\Computer Science\4º\VII Semestre\Complementos de Compilación\Tiger\Tiger\Parsing\Tiger.g4 by ANTLR 4.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="TigerParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6")]
[System.CLSCompliant(false)]
public interface ITigerVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>Program</c>
	/// labeled alternative in <see cref="TigerParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] TigerParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Call</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall([NotNull] TigerParser.CallContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenExprs</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenExprs([NotNull] TigerParser.ParenExprsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>For</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFor([NotNull] TigerParser.ForContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Break</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreak([NotNull] TigerParser.BreakContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Logical</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogical([NotNull] TigerParser.LogicalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>UnaryMinus</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryMinus([NotNull] TigerParser.UnaryMinusContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>String</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] TigerParser.StringContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>While</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhile([NotNull] TigerParser.WhileContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Integer</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInteger([NotNull] TigerParser.IntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Nil</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNil([NotNull] TigerParser.NilContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Array</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray([NotNull] TigerParser.ArrayContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Comparison</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] TigerParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Assign</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssign([NotNull] TigerParser.AssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Record</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecord([NotNull] TigerParser.RecordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Let</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLet([NotNull] TigerParser.LetContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LValue</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLValue([NotNull] TigerParser.LValueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>If</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf([NotNull] TigerParser.IfContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Arithmetic</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArithmetic([NotNull] TigerParser.ArithmeticContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IndexLValue</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexLValue([NotNull] TigerParser.IndexLValueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdLValue</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdLValue([NotNull] TigerParser.IdLValueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FieldLValue</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldLValue([NotNull] TigerParser.FieldLValueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeDecls</c>
	/// labeled alternative in <see cref="TigerParser.decls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDecls([NotNull] TigerParser.TypeDeclsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>VarDecl</c>
	/// labeled alternative in <see cref="TigerParser.decls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarDecl([NotNull] TigerParser.VarDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FuncDecls</c>
	/// labeled alternative in <see cref="TigerParser.decls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncDecls([NotNull] TigerParser.FuncDeclsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FuncDecl</c>
	/// labeled alternative in <see cref="TigerParser.func_decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncDecl([NotNull] TigerParser.FuncDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdType</c>
	/// labeled alternative in <see cref="TigerParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdType([NotNull] TigerParser.IdTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>RecordType</c>
	/// labeled alternative in <see cref="TigerParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecordType([NotNull] TigerParser.RecordTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayType</c>
	/// labeled alternative in <see cref="TigerParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayType([NotNull] TigerParser.ArrayTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeFields</c>
	/// labeled alternative in <see cref="TigerParser.type_fields"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeFields([NotNull] TigerParser.TypeFieldsContext context);
}

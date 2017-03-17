// Generated from D:\Zchool\Computer Science\4º\VII Semestre\Complementos de Compilación\Tiger\Tiger\Parsing\Tiger.g4 by ANTLR 4.6
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link TigerParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface TigerVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by the {@code Program}
	 * labeled alternative in {@link TigerParser#compileUnit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(TigerParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Call}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCall(TigerParser.CallContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ParenExprs}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParenExprs(TigerParser.ParenExprsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code For}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFor(TigerParser.ForContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Break}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBreak(TigerParser.BreakContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Logical}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogical(TigerParser.LogicalContext ctx);
	/**
	 * Visit a parse tree produced by the {@code UnaryMinus}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitUnaryMinus(TigerParser.UnaryMinusContext ctx);
	/**
	 * Visit a parse tree produced by the {@code String}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitString(TigerParser.StringContext ctx);
	/**
	 * Visit a parse tree produced by the {@code While}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWhile(TigerParser.WhileContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Integer}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInteger(TigerParser.IntegerContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Nil}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNil(TigerParser.NilContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Array}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArray(TigerParser.ArrayContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Comparison}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitComparison(TigerParser.ComparisonContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Assign}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssign(TigerParser.AssignContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Record}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRecord(TigerParser.RecordContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Let}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLet(TigerParser.LetContext ctx);
	/**
	 * Visit a parse tree produced by the {@code LValue}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLValue(TigerParser.LValueContext ctx);
	/**
	 * Visit a parse tree produced by the {@code If}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIf(TigerParser.IfContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Arithmetic}
	 * labeled alternative in {@link TigerParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArithmetic(TigerParser.ArithmeticContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IndexLValue}
	 * labeled alternative in {@link TigerParser#lvalue}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIndexLValue(TigerParser.IndexLValueContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdLValue}
	 * labeled alternative in {@link TigerParser#lvalue}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdLValue(TigerParser.IdLValueContext ctx);
	/**
	 * Visit a parse tree produced by the {@code FieldLValue}
	 * labeled alternative in {@link TigerParser#lvalue}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFieldLValue(TigerParser.FieldLValueContext ctx);
	/**
	 * Visit a parse tree produced by the {@code TypeDecls}
	 * labeled alternative in {@link TigerParser#decls}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTypeDecls(TigerParser.TypeDeclsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code VarDecl}
	 * labeled alternative in {@link TigerParser#decls}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVarDecl(TigerParser.VarDeclContext ctx);
	/**
	 * Visit a parse tree produced by the {@code FuncDecls}
	 * labeled alternative in {@link TigerParser#decls}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFuncDecls(TigerParser.FuncDeclsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code FuncDecl}
	 * labeled alternative in {@link TigerParser#func_decl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFuncDecl(TigerParser.FuncDeclContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdType}
	 * labeled alternative in {@link TigerParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdType(TigerParser.IdTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code RecordType}
	 * labeled alternative in {@link TigerParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRecordType(TigerParser.RecordTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ArrayType}
	 * labeled alternative in {@link TigerParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrayType(TigerParser.ArrayTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code TypeFields}
	 * labeled alternative in {@link TigerParser#type_fields}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTypeFields(TigerParser.TypeFieldsContext ctx);
}
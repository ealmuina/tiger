// Generated from D:\Zchool\Computer Science\4º\VII Semestre\Complementos de Compilación\Tiger\Tiger\Parsing\Tiger.g4 by ANTLR 4.6
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class TigerParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.6", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, STRING=41, INTEGER=42, ID=43, COMMENT=44, WS=45;
	public static final int
		RULE_compileUnit = 0, RULE_expr = 1, RULE_lvalue = 2, RULE_decls = 3, 
		RULE_func_decl = 4, RULE_type = 5, RULE_type_fields = 6;
	public static final String[] ruleNames = {
		"compileUnit", "expr", "lvalue", "decls", "func_decl", "type", "type_fields"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'nil'", "'-'", "'*'", "'/'", "'+'", "'<>'", "'='", "'>='", "'<='", 
		"'>'", "'<'", "'&'", "'|'", "':='", "'('", "','", "')'", "';'", "'{'", 
		"'}'", "'['", "']'", "'of'", "'if'", "'then'", "'else'", "'while'", "'do'", 
		"'for'", "'to'", "'break'", "'let'", "'in'", "'end'", "'.'", "'type'", 
		"'var'", "':'", "'function'", "'array'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, "STRING", "INTEGER", "ID", "COMMENT", "WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Tiger.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public TigerParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class CompileUnitContext extends ParserRuleContext {
		public CompileUnitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compileUnit; }
	 
		public CompileUnitContext() { }
		public void copyFrom(CompileUnitContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ProgramContext extends CompileUnitContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TerminalNode EOF() { return getToken(TigerParser.EOF, 0); }
		public ProgramContext(CompileUnitContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitProgram(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CompileUnitContext compileUnit() throws RecognitionException {
		CompileUnitContext _localctx = new CompileUnitContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_compileUnit);
		try {
			_localctx = new ProgramContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(14);
			expr(0);
			setState(15);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExprContext extends ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	 
		public ExprContext() { }
		public void copyFrom(ExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class CallContext extends ExprContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public CallContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitCall(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ParenExprsContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ParenExprsContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitParenExprs(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ForContext extends ExprContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ForContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitFor(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class BreakContext extends ExprContext {
		public BreakContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitBreak(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class LogicalContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public LogicalContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitLogical(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class UnaryMinusContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public UnaryMinusContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitUnaryMinus(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class StringContext extends ExprContext {
		public TerminalNode STRING() { return getToken(TigerParser.STRING, 0); }
		public StringContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitString(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class WhileContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public WhileContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitWhile(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class IntegerContext extends ExprContext {
		public TerminalNode INTEGER() { return getToken(TigerParser.INTEGER, 0); }
		public IntegerContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitInteger(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class NilContext extends ExprContext {
		public NilContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitNil(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ArrayContext extends ExprContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ArrayContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitArray(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ComparisonContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ComparisonContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitComparison(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class AssignContext extends ExprContext {
		public LvalueContext lvalue() {
			return getRuleContext(LvalueContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public AssignContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitAssign(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class RecordContext extends ExprContext {
		public Token typeID;
		public List<TerminalNode> ID() { return getTokens(TigerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(TigerParser.ID, i);
		}
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public RecordContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitRecord(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class LetContext extends ExprContext {
		public List<DeclsContext> decls() {
			return getRuleContexts(DeclsContext.class);
		}
		public DeclsContext decls(int i) {
			return getRuleContext(DeclsContext.class,i);
		}
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public LetContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitLet(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class LValueContext extends ExprContext {
		public LvalueContext lvalue() {
			return getRuleContext(LvalueContext.class,0);
		}
		public LValueContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitLValue(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class IfContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public IfContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitIf(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ArithmeticContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ArithmeticContext(ExprContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitArithmetic(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		enterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(119);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				{
				_localctx = new StringContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(18);
				match(STRING);
				}
				break;
			case 2:
				{
				_localctx = new IntegerContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(19);
				match(INTEGER);
				}
				break;
			case 3:
				{
				_localctx = new NilContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(20);
				match(T__0);
				}
				break;
			case 4:
				{
				_localctx = new LValueContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(21);
				lvalue(0);
				}
				break;
			case 5:
				{
				_localctx = new UnaryMinusContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(22);
				match(T__1);
				setState(23);
				expr(16);
				}
				break;
			case 6:
				{
				_localctx = new AssignContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(24);
				lvalue(0);
				setState(25);
				match(T__13);
				setState(26);
				expr(10);
				}
				break;
			case 7:
				{
				_localctx = new CallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(28);
				match(ID);
				setState(29);
				match(T__14);
				setState(38);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__14) | (1L << T__23) | (1L << T__26) | (1L << T__28) | (1L << T__30) | (1L << T__31) | (1L << STRING) | (1L << INTEGER) | (1L << ID))) != 0)) {
					{
					setState(30);
					expr(0);
					setState(35);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__15) {
						{
						{
						setState(31);
						match(T__15);
						setState(32);
						expr(0);
						}
						}
						setState(37);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(40);
				match(T__16);
				}
				break;
			case 8:
				{
				_localctx = new ParenExprsContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(41);
				match(T__14);
				setState(50);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__14) | (1L << T__23) | (1L << T__26) | (1L << T__28) | (1L << T__30) | (1L << T__31) | (1L << STRING) | (1L << INTEGER) | (1L << ID))) != 0)) {
					{
					setState(42);
					expr(0);
					setState(47);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__17) {
						{
						{
						setState(43);
						match(T__17);
						setState(44);
						expr(0);
						}
						}
						setState(49);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(52);
				match(T__16);
				}
				break;
			case 9:
				{
				_localctx = new RecordContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(53);
				((RecordContext)_localctx).typeID = match(ID);
				setState(54);
				match(T__18);
				setState(67);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==ID) {
					{
					setState(55);
					match(ID);
					setState(56);
					match(T__6);
					setState(57);
					expr(0);
					setState(64);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__15) {
						{
						{
						setState(58);
						match(T__15);
						setState(59);
						match(ID);
						setState(60);
						match(T__6);
						setState(61);
						expr(0);
						}
						}
						setState(66);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(69);
				match(T__19);
				}
				break;
			case 10:
				{
				_localctx = new ArrayContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(70);
				match(ID);
				setState(71);
				match(T__20);
				setState(72);
				expr(0);
				setState(73);
				match(T__21);
				setState(74);
				match(T__22);
				setState(75);
				expr(6);
				}
				break;
			case 11:
				{
				_localctx = new IfContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(77);
				match(T__23);
				setState(78);
				expr(0);
				setState(79);
				match(T__24);
				setState(80);
				expr(0);
				setState(83);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
				case 1:
					{
					setState(81);
					match(T__25);
					setState(82);
					expr(0);
					}
					break;
				}
				}
				break;
			case 12:
				{
				_localctx = new WhileContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(85);
				match(T__26);
				setState(86);
				expr(0);
				setState(87);
				match(T__27);
				setState(88);
				expr(4);
				}
				break;
			case 13:
				{
				_localctx = new ForContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(90);
				match(T__28);
				setState(91);
				match(ID);
				setState(92);
				match(T__13);
				setState(93);
				expr(0);
				setState(94);
				match(T__29);
				setState(95);
				expr(0);
				setState(96);
				match(T__27);
				setState(97);
				expr(3);
				}
				break;
			case 14:
				{
				_localctx = new BreakContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(99);
				match(T__30);
				}
				break;
			case 15:
				{
				_localctx = new LetContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(100);
				match(T__31);
				setState(102); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(101);
					decls();
					}
					}
					setState(104); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__35) | (1L << T__36) | (1L << T__38))) != 0) );
				setState(106);
				match(T__32);
				setState(115);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__14) | (1L << T__23) | (1L << T__26) | (1L << T__28) | (1L << T__30) | (1L << T__31) | (1L << STRING) | (1L << INTEGER) | (1L << ID))) != 0)) {
					{
					setState(107);
					expr(0);
					setState(112);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__17) {
						{
						{
						setState(108);
						match(T__17);
						setState(109);
						expr(0);
						}
						}
						setState(114);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(117);
				match(T__33);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(138);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(136);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
					case 1:
						{
						_localctx = new ArithmeticContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(121);
						if (!(precpred(_ctx, 15))) throw new FailedPredicateException(this, "precpred(_ctx, 15)");
						setState(122);
						((ArithmeticContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
							((ArithmeticContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(123);
						expr(16);
						}
						break;
					case 2:
						{
						_localctx = new ArithmeticContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(124);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(125);
						((ArithmeticContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__1 || _la==T__4) ) {
							((ArithmeticContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(126);
						expr(15);
						}
						break;
					case 3:
						{
						_localctx = new ComparisonContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(127);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(128);
						((ComparisonContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10))) != 0)) ) {
							((ComparisonContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(129);
						expr(14);
						}
						break;
					case 4:
						{
						_localctx = new LogicalContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(130);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(131);
						((LogicalContext)_localctx).op = match(T__11);
						setState(132);
						expr(13);
						}
						break;
					case 5:
						{
						_localctx = new LogicalContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(133);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(134);
						((LogicalContext)_localctx).op = match(T__12);
						setState(135);
						expr(12);
						}
						break;
					}
					} 
				}
				setState(140);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class LvalueContext extends ParserRuleContext {
		public LvalueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_lvalue; }
	 
		public LvalueContext() { }
		public void copyFrom(LvalueContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class IndexLValueContext extends LvalueContext {
		public LvalueContext lvalue() {
			return getRuleContext(LvalueContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public IndexLValueContext(LvalueContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitIndexLValue(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class IdLValueContext extends LvalueContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public IdLValueContext(LvalueContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitIdLValue(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class FieldLValueContext extends LvalueContext {
		public LvalueContext lvalue() {
			return getRuleContext(LvalueContext.class,0);
		}
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public FieldLValueContext(LvalueContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitFieldLValue(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LvalueContext lvalue() throws RecognitionException {
		return lvalue(0);
	}

	private LvalueContext lvalue(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		LvalueContext _localctx = new LvalueContext(_ctx, _parentState);
		LvalueContext _prevctx = _localctx;
		int _startState = 4;
		enterRecursionRule(_localctx, 4, RULE_lvalue, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			_localctx = new IdLValueContext(_localctx);
			_ctx = _localctx;
			_prevctx = _localctx;

			setState(142);
			match(ID);
			}
			_ctx.stop = _input.LT(-1);
			setState(154);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(152);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
					case 1:
						{
						_localctx = new FieldLValueContext(new LvalueContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_lvalue);
						setState(144);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(145);
						match(T__34);
						setState(146);
						match(ID);
						}
						break;
					case 2:
						{
						_localctx = new IndexLValueContext(new LvalueContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_lvalue);
						setState(147);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(148);
						match(T__20);
						setState(149);
						expr(0);
						setState(150);
						match(T__21);
						}
						break;
					}
					} 
				}
				setState(156);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class DeclsContext extends ParserRuleContext {
		public DeclsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_decls; }
	 
		public DeclsContext() { }
		public void copyFrom(DeclsContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class VarDeclContext extends DeclsContext {
		public Token id;
		public Token typeId;
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<TerminalNode> ID() { return getTokens(TigerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(TigerParser.ID, i);
		}
		public VarDeclContext(DeclsContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitVarDecl(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class TypeDeclsContext extends DeclsContext {
		public List<TerminalNode> ID() { return getTokens(TigerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(TigerParser.ID, i);
		}
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TypeDeclsContext(DeclsContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitTypeDecls(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class FuncDeclsContext extends DeclsContext {
		public List<Func_declContext> func_decl() {
			return getRuleContexts(Func_declContext.class);
		}
		public Func_declContext func_decl(int i) {
			return getRuleContext(Func_declContext.class,i);
		}
		public FuncDeclsContext(DeclsContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitFuncDecls(this);
			else return visitor.visitChildren(this);
		}
	}

	public final DeclsContext decls() throws RecognitionException {
		DeclsContext _localctx = new DeclsContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_decls);
		int _la;
		try {
			int _alt;
			setState(178);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__35:
				_localctx = new TypeDeclsContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(161); 
				_errHandler.sync(this);
				_alt = 1;
				do {
					switch (_alt) {
					case 1:
						{
						{
						setState(157);
						match(T__35);
						setState(158);
						match(ID);
						setState(159);
						match(T__6);
						setState(160);
						type();
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					setState(163); 
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,15,_ctx);
				} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
				}
				break;
			case T__36:
				_localctx = new VarDeclContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(165);
				match(T__36);
				setState(166);
				((VarDeclContext)_localctx).id = match(ID);
				setState(169);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__37) {
					{
					setState(167);
					match(T__37);
					setState(168);
					((VarDeclContext)_localctx).typeId = match(ID);
					}
				}

				setState(171);
				match(T__13);
				setState(172);
				expr(0);
				}
				break;
			case T__38:
				_localctx = new FuncDeclsContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(174); 
				_errHandler.sync(this);
				_alt = 1;
				do {
					switch (_alt) {
					case 1:
						{
						{
						setState(173);
						func_decl();
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					setState(176); 
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,17,_ctx);
				} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Func_declContext extends ParserRuleContext {
		public Func_declContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_func_decl; }
	 
		public Func_declContext() { }
		public void copyFrom(Func_declContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class FuncDeclContext extends Func_declContext {
		public Token id;
		public Token typeId;
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<TerminalNode> ID() { return getTokens(TigerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(TigerParser.ID, i);
		}
		public Type_fieldsContext type_fields() {
			return getRuleContext(Type_fieldsContext.class,0);
		}
		public FuncDeclContext(Func_declContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitFuncDecl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Func_declContext func_decl() throws RecognitionException {
		Func_declContext _localctx = new Func_declContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_func_decl);
		int _la;
		try {
			_localctx = new FuncDeclContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(180);
			match(T__38);
			setState(181);
			((FuncDeclContext)_localctx).id = match(ID);
			setState(182);
			match(T__14);
			setState(184);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ID) {
				{
				setState(183);
				type_fields();
				}
			}

			setState(186);
			match(T__16);
			setState(189);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__37) {
				{
				setState(187);
				match(T__37);
				setState(188);
				((FuncDeclContext)_localctx).typeId = match(ID);
				}
			}

			setState(191);
			match(T__6);
			setState(192);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	 
		public TypeContext() { }
		public void copyFrom(TypeContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ArrayTypeContext extends TypeContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public ArrayTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitArrayType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class RecordTypeContext extends TypeContext {
		public Type_fieldsContext type_fields() {
			return getRuleContext(Type_fieldsContext.class,0);
		}
		public RecordTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitRecordType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class IdTypeContext extends TypeContext {
		public TerminalNode ID() { return getToken(TigerParser.ID, 0); }
		public IdTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitIdType(this);
			else return visitor.visitChildren(this);
		}
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_type);
		int _la;
		try {
			setState(203);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				_localctx = new IdTypeContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(194);
				match(ID);
				}
				break;
			case T__18:
				_localctx = new RecordTypeContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(195);
				match(T__18);
				setState(197);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==ID) {
					{
					setState(196);
					type_fields();
					}
				}

				setState(199);
				match(T__19);
				}
				break;
			case T__39:
				_localctx = new ArrayTypeContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(200);
				match(T__39);
				setState(201);
				match(T__22);
				setState(202);
				match(ID);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_fieldsContext extends ParserRuleContext {
		public Type_fieldsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_fields; }
	 
		public Type_fieldsContext() { }
		public void copyFrom(Type_fieldsContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class TypeFieldsContext extends Type_fieldsContext {
		public List<TerminalNode> ID() { return getTokens(TigerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(TigerParser.ID, i);
		}
		public TypeFieldsContext(Type_fieldsContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof TigerVisitor ) return ((TigerVisitor<? extends T>)visitor).visitTypeFields(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Type_fieldsContext type_fields() throws RecognitionException {
		Type_fieldsContext _localctx = new Type_fieldsContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_type_fields);
		int _la;
		try {
			_localctx = new TypeFieldsContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(205);
			match(ID);
			setState(206);
			match(T__37);
			setState(207);
			match(ID);
			setState(214);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__15) {
				{
				{
				setState(208);
				match(T__15);
				setState(209);
				match(ID);
				setState(210);
				match(T__37);
				setState(211);
				match(ID);
				}
				}
				setState(216);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1:
			return expr_sempred((ExprContext)_localctx, predIndex);
		case 2:
			return lvalue_sempred((LvalueContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 15);
		case 1:
			return precpred(_ctx, 14);
		case 2:
			return precpred(_ctx, 13);
		case 3:
			return precpred(_ctx, 12);
		case 4:
			return precpred(_ctx, 11);
		}
		return true;
	}
	private boolean lvalue_sempred(LvalueContext _localctx, int predIndex) {
		switch (predIndex) {
		case 5:
			return precpred(_ctx, 2);
		case 6:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u0430\ud6d1\u8206\uad2d\u4417\uaef1\u8d80\uaadd\3/\u00dc\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\3\2\3\2\3\2\3\3\3\3\3\3"+
		"\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\7\3$\n\3\f\3\16\3"+
		"\'\13\3\5\3)\n\3\3\3\3\3\3\3\3\3\3\3\7\3\60\n\3\f\3\16\3\63\13\3\5\3\65"+
		"\n\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\7\3A\n\3\f\3\16\3D\13\3\5"+
		"\3F\n\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\5\3V\n"+
		"\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3"+
		"\6\3i\n\3\r\3\16\3j\3\3\3\3\3\3\3\3\7\3q\n\3\f\3\16\3t\13\3\5\3v\n\3\3"+
		"\3\3\3\5\3z\n\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3"+
		"\3\3\3\7\3\u008b\n\3\f\3\16\3\u008e\13\3\3\4\3\4\3\4\3\4\3\4\3\4\3\4\3"+
		"\4\3\4\3\4\3\4\7\4\u009b\n\4\f\4\16\4\u009e\13\4\3\5\3\5\3\5\3\5\6\5\u00a4"+
		"\n\5\r\5\16\5\u00a5\3\5\3\5\3\5\3\5\5\5\u00ac\n\5\3\5\3\5\3\5\6\5\u00b1"+
		"\n\5\r\5\16\5\u00b2\5\5\u00b5\n\5\3\6\3\6\3\6\3\6\5\6\u00bb\n\6\3\6\3"+
		"\6\3\6\5\6\u00c0\n\6\3\6\3\6\3\6\3\7\3\7\3\7\5\7\u00c8\n\7\3\7\3\7\3\7"+
		"\3\7\5\7\u00ce\n\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\7\b\u00d7\n\b\f\b\16\b"+
		"\u00da\13\b\3\b\2\4\4\6\t\2\4\6\b\n\f\16\2\5\3\2\5\6\4\2\4\4\7\7\3\2\b"+
		"\r\u00fe\2\20\3\2\2\2\4y\3\2\2\2\6\u008f\3\2\2\2\b\u00b4\3\2\2\2\n\u00b6"+
		"\3\2\2\2\f\u00cd\3\2\2\2\16\u00cf\3\2\2\2\20\21\5\4\3\2\21\22\7\2\2\3"+
		"\22\3\3\2\2\2\23\24\b\3\1\2\24z\7+\2\2\25z\7,\2\2\26z\7\3\2\2\27z\5\6"+
		"\4\2\30\31\7\4\2\2\31z\5\4\3\22\32\33\5\6\4\2\33\34\7\20\2\2\34\35\5\4"+
		"\3\f\35z\3\2\2\2\36\37\7-\2\2\37(\7\21\2\2 %\5\4\3\2!\"\7\22\2\2\"$\5"+
		"\4\3\2#!\3\2\2\2$\'\3\2\2\2%#\3\2\2\2%&\3\2\2\2&)\3\2\2\2\'%\3\2\2\2("+
		" \3\2\2\2()\3\2\2\2)*\3\2\2\2*z\7\23\2\2+\64\7\21\2\2,\61\5\4\3\2-.\7"+
		"\24\2\2.\60\5\4\3\2/-\3\2\2\2\60\63\3\2\2\2\61/\3\2\2\2\61\62\3\2\2\2"+
		"\62\65\3\2\2\2\63\61\3\2\2\2\64,\3\2\2\2\64\65\3\2\2\2\65\66\3\2\2\2\66"+
		"z\7\23\2\2\678\7-\2\28E\7\25\2\29:\7-\2\2:;\7\t\2\2;B\5\4\3\2<=\7\22\2"+
		"\2=>\7-\2\2>?\7\t\2\2?A\5\4\3\2@<\3\2\2\2AD\3\2\2\2B@\3\2\2\2BC\3\2\2"+
		"\2CF\3\2\2\2DB\3\2\2\2E9\3\2\2\2EF\3\2\2\2FG\3\2\2\2Gz\7\26\2\2HI\7-\2"+
		"\2IJ\7\27\2\2JK\5\4\3\2KL\7\30\2\2LM\7\31\2\2MN\5\4\3\bNz\3\2\2\2OP\7"+
		"\32\2\2PQ\5\4\3\2QR\7\33\2\2RU\5\4\3\2ST\7\34\2\2TV\5\4\3\2US\3\2\2\2"+
		"UV\3\2\2\2Vz\3\2\2\2WX\7\35\2\2XY\5\4\3\2YZ\7\36\2\2Z[\5\4\3\6[z\3\2\2"+
		"\2\\]\7\37\2\2]^\7-\2\2^_\7\20\2\2_`\5\4\3\2`a\7 \2\2ab\5\4\3\2bc\7\36"+
		"\2\2cd\5\4\3\5dz\3\2\2\2ez\7!\2\2fh\7\"\2\2gi\5\b\5\2hg\3\2\2\2ij\3\2"+
		"\2\2jh\3\2\2\2jk\3\2\2\2kl\3\2\2\2lu\7#\2\2mr\5\4\3\2no\7\24\2\2oq\5\4"+
		"\3\2pn\3\2\2\2qt\3\2\2\2rp\3\2\2\2rs\3\2\2\2sv\3\2\2\2tr\3\2\2\2um\3\2"+
		"\2\2uv\3\2\2\2vw\3\2\2\2wx\7$\2\2xz\3\2\2\2y\23\3\2\2\2y\25\3\2\2\2y\26"+
		"\3\2\2\2y\27\3\2\2\2y\30\3\2\2\2y\32\3\2\2\2y\36\3\2\2\2y+\3\2\2\2y\67"+
		"\3\2\2\2yH\3\2\2\2yO\3\2\2\2yW\3\2\2\2y\\\3\2\2\2ye\3\2\2\2yf\3\2\2\2"+
		"z\u008c\3\2\2\2{|\f\21\2\2|}\t\2\2\2}\u008b\5\4\3\22~\177\f\20\2\2\177"+
		"\u0080\t\3\2\2\u0080\u008b\5\4\3\21\u0081\u0082\f\17\2\2\u0082\u0083\t"+
		"\4\2\2\u0083\u008b\5\4\3\20\u0084\u0085\f\16\2\2\u0085\u0086\7\16\2\2"+
		"\u0086\u008b\5\4\3\17\u0087\u0088\f\r\2\2\u0088\u0089\7\17\2\2\u0089\u008b"+
		"\5\4\3\16\u008a{\3\2\2\2\u008a~\3\2\2\2\u008a\u0081\3\2\2\2\u008a\u0084"+
		"\3\2\2\2\u008a\u0087\3\2\2\2\u008b\u008e\3\2\2\2\u008c\u008a\3\2\2\2\u008c"+
		"\u008d\3\2\2\2\u008d\5\3\2\2\2\u008e\u008c\3\2\2\2\u008f\u0090\b\4\1\2"+
		"\u0090\u0091\7-\2\2\u0091\u009c\3\2\2\2\u0092\u0093\f\4\2\2\u0093\u0094"+
		"\7%\2\2\u0094\u009b\7-\2\2\u0095\u0096\f\3\2\2\u0096\u0097\7\27\2\2\u0097"+
		"\u0098\5\4\3\2\u0098\u0099\7\30\2\2\u0099\u009b\3\2\2\2\u009a\u0092\3"+
		"\2\2\2\u009a\u0095\3\2\2\2\u009b\u009e\3\2\2\2\u009c\u009a\3\2\2\2\u009c"+
		"\u009d\3\2\2\2\u009d\7\3\2\2\2\u009e\u009c\3\2\2\2\u009f\u00a0\7&\2\2"+
		"\u00a0\u00a1\7-\2\2\u00a1\u00a2\7\t\2\2\u00a2\u00a4\5\f\7\2\u00a3\u009f"+
		"\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5\u00a3\3\2\2\2\u00a5\u00a6\3\2\2\2\u00a6"+
		"\u00b5\3\2\2\2\u00a7\u00a8\7\'\2\2\u00a8\u00ab\7-\2\2\u00a9\u00aa\7(\2"+
		"\2\u00aa\u00ac\7-\2\2\u00ab\u00a9\3\2\2\2\u00ab\u00ac\3\2\2\2\u00ac\u00ad"+
		"\3\2\2\2\u00ad\u00ae\7\20\2\2\u00ae\u00b5\5\4\3\2\u00af\u00b1\5\n\6\2"+
		"\u00b0\u00af\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2\u00b0\3\2\2\2\u00b2\u00b3"+
		"\3\2\2\2\u00b3\u00b5\3\2\2\2\u00b4\u00a3\3\2\2\2\u00b4\u00a7\3\2\2\2\u00b4"+
		"\u00b0\3\2\2\2\u00b5\t\3\2\2\2\u00b6\u00b7\7)\2\2\u00b7\u00b8\7-\2\2\u00b8"+
		"\u00ba\7\21\2\2\u00b9\u00bb\5\16\b\2\u00ba\u00b9\3\2\2\2\u00ba\u00bb\3"+
		"\2\2\2\u00bb\u00bc\3\2\2\2\u00bc\u00bf\7\23\2\2\u00bd\u00be\7(\2\2\u00be"+
		"\u00c0\7-\2\2\u00bf\u00bd\3\2\2\2\u00bf\u00c0\3\2\2\2\u00c0\u00c1\3\2"+
		"\2\2\u00c1\u00c2\7\t\2\2\u00c2\u00c3\5\4\3\2\u00c3\13\3\2\2\2\u00c4\u00ce"+
		"\7-\2\2\u00c5\u00c7\7\25\2\2\u00c6\u00c8\5\16\b\2\u00c7\u00c6\3\2\2\2"+
		"\u00c7\u00c8\3\2\2\2\u00c8\u00c9\3\2\2\2\u00c9\u00ce\7\26\2\2\u00ca\u00cb"+
		"\7*\2\2\u00cb\u00cc\7\31\2\2\u00cc\u00ce\7-\2\2\u00cd\u00c4\3\2\2\2\u00cd"+
		"\u00c5\3\2\2\2\u00cd\u00ca\3\2\2\2\u00ce\r\3\2\2\2\u00cf\u00d0\7-\2\2"+
		"\u00d0\u00d1\7(\2\2\u00d1\u00d8\7-\2\2\u00d2\u00d3\7\22\2\2\u00d3\u00d4"+
		"\7-\2\2\u00d4\u00d5\7(\2\2\u00d5\u00d7\7-\2\2\u00d6\u00d2\3\2\2\2\u00d7"+
		"\u00da\3\2\2\2\u00d8\u00d6\3\2\2\2\u00d8\u00d9\3\2\2\2\u00d9\17\3\2\2"+
		"\2\u00da\u00d8\3\2\2\2\32%(\61\64BEUjruy\u008a\u008c\u009a\u009c\u00a5"+
		"\u00ab\u00b2\u00b4\u00ba\u00bf\u00c7\u00cd\u00d8";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}
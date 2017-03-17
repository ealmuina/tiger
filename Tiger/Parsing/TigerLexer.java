// Generated from D:\Zchool\Computer Science\4º\VII Semestre\Complementos de Compilación\Tiger\Tiger\Parsing\Tiger.g4 by ANTLR 4.6
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class TigerLexer extends Lexer {
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
	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "LETTER", 
		"DIGIT", "ASCII", "ESCAPE_SEQ", "CHAR", "EMPTY", "STRING", "INTEGER", 
		"ID", "COMMENT", "WS"
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


	public TigerLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Tiger.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u0430\ud6d1\u8206\uad2d\u4417\uaef1\u8d80\uaadd\2/\u013a\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\3\2\3\2\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\7\3\b\3"+
		"\b\3\t\3\t\3\t\3\n\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3"+
		"\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\23\3\23\3\24\3\24\3\25\3\25\3"+
		"\26\3\26\3\27\3\27\3\30\3\30\3\30\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3"+
		"\32\3\33\3\33\3\33\3\33\3\33\3\34\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3"+
		"\35\3\36\3\36\3\36\3\36\3\37\3\37\3\37\3 \3 \3 \3 \3 \3 \3!\3!\3!\3!\3"+
		"\"\3\"\3\"\3#\3#\3#\3#\3$\3$\3%\3%\3%\3%\3%\3&\3&\3&\3&\3\'\3\'\3(\3("+
		"\3(\3(\3(\3(\3(\3(\3(\3)\3)\3)\3)\3)\3)\3*\3*\3+\3+\3,\3,\3,\3,\3,\3,"+
		"\3,\3,\3,\5,\u00f6\n,\5,\u00f8\n,\3-\3-\3-\7-\u00fd\n-\f-\16-\u0100\13"+
		"-\3-\3-\5-\u0104\n-\3.\5.\u0107\n.\3.\5.\u010a\n.\3/\3/\3\60\3\60\7\60"+
		"\u0110\n\60\f\60\16\60\u0113\13\60\3\60\3\60\3\61\6\61\u0118\n\61\r\61"+
		"\16\61\u0119\3\62\3\62\3\62\3\62\7\62\u0120\n\62\f\62\16\62\u0123\13\62"+
		"\3\63\3\63\3\63\3\63\3\63\7\63\u012a\n\63\f\63\16\63\u012d\13\63\3\63"+
		"\3\63\3\63\3\63\3\63\3\64\6\64\u0135\n\64\r\64\16\64\u0136\3\64\3\64\3"+
		"\u012b\2\65\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33"+
		"\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67"+
		"\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S\2U\2W\2Y\2[\2]\2_+a,c-e.g/\3\2\t"+
		"\4\2C\\c|\3\2\62;\3\2\62\63\3\2\629\6\2$$ppttvv\5\2\"#%]_\u0080\5\2\13"+
		"\f\17\17\"\"\u0141\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13"+
		"\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2"+
		"\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2"+
		"!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3"+
		"\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2"+
		"\29\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E"+
		"\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2"+
		"\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2\3i\3\2\2\2"+
		"\5m\3\2\2\2\7o\3\2\2\2\tq\3\2\2\2\13s\3\2\2\2\ru\3\2\2\2\17x\3\2\2\2\21"+
		"z\3\2\2\2\23}\3\2\2\2\25\u0080\3\2\2\2\27\u0082\3\2\2\2\31\u0084\3\2\2"+
		"\2\33\u0086\3\2\2\2\35\u0088\3\2\2\2\37\u008b\3\2\2\2!\u008d\3\2\2\2#"+
		"\u008f\3\2\2\2%\u0091\3\2\2\2\'\u0093\3\2\2\2)\u0095\3\2\2\2+\u0097\3"+
		"\2\2\2-\u0099\3\2\2\2/\u009b\3\2\2\2\61\u009e\3\2\2\2\63\u00a1\3\2\2\2"+
		"\65\u00a6\3\2\2\2\67\u00ab\3\2\2\29\u00b1\3\2\2\2;\u00b4\3\2\2\2=\u00b8"+
		"\3\2\2\2?\u00bb\3\2\2\2A\u00c1\3\2\2\2C\u00c5\3\2\2\2E\u00c8\3\2\2\2G"+
		"\u00cc\3\2\2\2I\u00ce\3\2\2\2K\u00d3\3\2\2\2M\u00d7\3\2\2\2O\u00d9\3\2"+
		"\2\2Q\u00e2\3\2\2\2S\u00e8\3\2\2\2U\u00ea\3\2\2\2W\u00f7\3\2\2\2Y\u00f9"+
		"\3\2\2\2[\u0109\3\2\2\2]\u010b\3\2\2\2_\u010d\3\2\2\2a\u0117\3\2\2\2c"+
		"\u011b\3\2\2\2e\u0124\3\2\2\2g\u0134\3\2\2\2ij\7p\2\2jk\7k\2\2kl\7n\2"+
		"\2l\4\3\2\2\2mn\7/\2\2n\6\3\2\2\2op\7,\2\2p\b\3\2\2\2qr\7\61\2\2r\n\3"+
		"\2\2\2st\7-\2\2t\f\3\2\2\2uv\7>\2\2vw\7@\2\2w\16\3\2\2\2xy\7?\2\2y\20"+
		"\3\2\2\2z{\7@\2\2{|\7?\2\2|\22\3\2\2\2}~\7>\2\2~\177\7?\2\2\177\24\3\2"+
		"\2\2\u0080\u0081\7@\2\2\u0081\26\3\2\2\2\u0082\u0083\7>\2\2\u0083\30\3"+
		"\2\2\2\u0084\u0085\7(\2\2\u0085\32\3\2\2\2\u0086\u0087\7~\2\2\u0087\34"+
		"\3\2\2\2\u0088\u0089\7<\2\2\u0089\u008a\7?\2\2\u008a\36\3\2\2\2\u008b"+
		"\u008c\7*\2\2\u008c \3\2\2\2\u008d\u008e\7.\2\2\u008e\"\3\2\2\2\u008f"+
		"\u0090\7+\2\2\u0090$\3\2\2\2\u0091\u0092\7=\2\2\u0092&\3\2\2\2\u0093\u0094"+
		"\7}\2\2\u0094(\3\2\2\2\u0095\u0096\7\177\2\2\u0096*\3\2\2\2\u0097\u0098"+
		"\7]\2\2\u0098,\3\2\2\2\u0099\u009a\7_\2\2\u009a.\3\2\2\2\u009b\u009c\7"+
		"q\2\2\u009c\u009d\7h\2\2\u009d\60\3\2\2\2\u009e\u009f\7k\2\2\u009f\u00a0"+
		"\7h\2\2\u00a0\62\3\2\2\2\u00a1\u00a2\7v\2\2\u00a2\u00a3\7j\2\2\u00a3\u00a4"+
		"\7g\2\2\u00a4\u00a5\7p\2\2\u00a5\64\3\2\2\2\u00a6\u00a7\7g\2\2\u00a7\u00a8"+
		"\7n\2\2\u00a8\u00a9\7u\2\2\u00a9\u00aa\7g\2\2\u00aa\66\3\2\2\2\u00ab\u00ac"+
		"\7y\2\2\u00ac\u00ad\7j\2\2\u00ad\u00ae\7k\2\2\u00ae\u00af\7n\2\2\u00af"+
		"\u00b0\7g\2\2\u00b08\3\2\2\2\u00b1\u00b2\7f\2\2\u00b2\u00b3\7q\2\2\u00b3"+
		":\3\2\2\2\u00b4\u00b5\7h\2\2\u00b5\u00b6\7q\2\2\u00b6\u00b7\7t\2\2\u00b7"+
		"<\3\2\2\2\u00b8\u00b9\7v\2\2\u00b9\u00ba\7q\2\2\u00ba>\3\2\2\2\u00bb\u00bc"+
		"\7d\2\2\u00bc\u00bd\7t\2\2\u00bd\u00be\7g\2\2\u00be\u00bf\7c\2\2\u00bf"+
		"\u00c0\7m\2\2\u00c0@\3\2\2\2\u00c1\u00c2\7n\2\2\u00c2\u00c3\7g\2\2\u00c3"+
		"\u00c4\7v\2\2\u00c4B\3\2\2\2\u00c5\u00c6\7k\2\2\u00c6\u00c7\7p\2\2\u00c7"+
		"D\3\2\2\2\u00c8\u00c9\7g\2\2\u00c9\u00ca\7p\2\2\u00ca\u00cb\7f\2\2\u00cb"+
		"F\3\2\2\2\u00cc\u00cd\7\60\2\2\u00cdH\3\2\2\2\u00ce\u00cf\7v\2\2\u00cf"+
		"\u00d0\7{\2\2\u00d0\u00d1\7r\2\2\u00d1\u00d2\7g\2\2\u00d2J\3\2\2\2\u00d3"+
		"\u00d4\7x\2\2\u00d4\u00d5\7c\2\2\u00d5\u00d6\7t\2\2\u00d6L\3\2\2\2\u00d7"+
		"\u00d8\7<\2\2\u00d8N\3\2\2\2\u00d9\u00da\7h\2\2\u00da\u00db\7w\2\2\u00db"+
		"\u00dc\7p\2\2\u00dc\u00dd\7e\2\2\u00dd\u00de\7v\2\2\u00de\u00df\7k\2\2"+
		"\u00df\u00e0\7q\2\2\u00e0\u00e1\7p\2\2\u00e1P\3\2\2\2\u00e2\u00e3\7c\2"+
		"\2\u00e3\u00e4\7t\2\2\u00e4\u00e5\7t\2\2\u00e5\u00e6\7c\2\2\u00e6\u00e7"+
		"\7{\2\2\u00e7R\3\2\2\2\u00e8\u00e9\t\2\2\2\u00e9T\3\2\2\2\u00ea\u00eb"+
		"\t\3\2\2\u00ebV\3\2\2\2\u00ec\u00ed\7\62\2\2\u00ed\u00ee\5U+\2\u00ee\u00ef"+
		"\5U+\2\u00ef\u00f8\3\2\2\2\u00f0\u00f5\7\63\2\2\u00f1\u00f2\t\4\2\2\u00f2"+
		"\u00f6\5U+\2\u00f3\u00f4\7\64\2\2\u00f4\u00f6\t\5\2\2\u00f5\u00f1\3\2"+
		"\2\2\u00f5\u00f3\3\2\2\2\u00f6\u00f8\3\2\2\2\u00f7\u00ec\3\2\2\2\u00f7"+
		"\u00f0\3\2\2\2\u00f8X\3\2\2\2\u00f9\u0103\7^\2\2\u00fa\u0104\t\6\2\2\u00fb"+
		"\u00fd\5]/\2\u00fc\u00fb\3\2\2\2\u00fd\u0100\3\2\2\2\u00fe\u00fc\3\2\2"+
		"\2\u00fe\u00ff\3\2\2\2\u00ff\u0101\3\2\2\2\u0100\u00fe\3\2\2\2\u0101\u0104"+
		"\7^\2\2\u0102\u0104\5W,\2\u0103\u00fa\3\2\2\2\u0103\u00fe\3\2\2\2\u0103"+
		"\u0102\3\2\2\2\u0104Z\3\2\2\2\u0105\u0107\t\7\2\2\u0106\u0105\3\2\2\2"+
		"\u0107\u010a\3\2\2\2\u0108\u010a\5Y-\2\u0109\u0106\3\2\2\2\u0109\u0108"+
		"\3\2\2\2\u010a\\\3\2\2\2\u010b\u010c\t\b\2\2\u010c^\3\2\2\2\u010d\u0111"+
		"\7$\2\2\u010e\u0110\5[.\2\u010f\u010e\3\2\2\2\u0110\u0113\3\2\2\2\u0111"+
		"\u010f\3\2\2\2\u0111\u0112\3\2\2\2\u0112\u0114\3\2\2\2\u0113\u0111\3\2"+
		"\2\2\u0114\u0115\7$\2\2\u0115`\3\2\2\2\u0116\u0118\5U+\2\u0117\u0116\3"+
		"\2\2\2\u0118\u0119\3\2\2\2\u0119\u0117\3\2\2\2\u0119\u011a\3\2\2\2\u011a"+
		"b\3\2\2\2\u011b\u0121\5S*\2\u011c\u0120\5S*\2\u011d\u0120\5U+\2\u011e"+
		"\u0120\7a\2\2\u011f\u011c\3\2\2\2\u011f\u011d\3\2\2\2\u011f\u011e\3\2"+
		"\2\2\u0120\u0123\3\2\2\2\u0121\u011f\3\2\2\2\u0121\u0122\3\2\2\2\u0122"+
		"d\3\2\2\2\u0123\u0121\3\2\2\2\u0124\u0125\7\61\2\2\u0125\u0126\7,\2\2"+
		"\u0126\u012b\3\2\2\2\u0127\u012a\5e\63\2\u0128\u012a\13\2\2\2\u0129\u0127"+
		"\3\2\2\2\u0129\u0128\3\2\2\2\u012a\u012d\3\2\2\2\u012b\u012c\3\2\2\2\u012b"+
		"\u0129\3\2\2\2\u012c\u012e\3\2\2\2\u012d\u012b\3\2\2\2\u012e\u012f\7,"+
		"\2\2\u012f\u0130\7\61\2\2\u0130\u0131\3\2\2\2\u0131\u0132\b\63\2\2\u0132"+
		"f\3\2\2\2\u0133\u0135\5]/\2\u0134\u0133\3\2\2\2\u0135\u0136\3\2\2\2\u0136"+
		"\u0134\3\2\2\2\u0136\u0137\3\2\2\2\u0137\u0138\3\2\2\2\u0138\u0139\b\64"+
		"\2\2\u0139h\3\2\2\2\20\2\u00f5\u00f7\u00fe\u0103\u0106\u0109\u0111\u0119"+
		"\u011f\u0121\u0129\u012b\u0136\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}
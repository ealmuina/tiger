using System.Collections.Generic;
using Antlr4.Runtime;

namespace Tiger.Parsing
{
    class LexerErrorListener : IAntlrErrorListener<int>
    {
        List<string> errors;

        public LexerErrorListener(List<string> errors)
        {
            this.errors = errors;
        }

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errors.Add(string.Format("({0},{1}): {2}", line, charPositionInLine, msg));
        }
    }

    class ParserErrorListener : BaseErrorListener
    {
        List<string> errors;

        public ParserErrorListener(List<string> errors)
        {
            this.errors = errors;
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errors.Add(string.Format("({0},{1}): {2}", line, charPositionInLine, msg));
        }
    }
}

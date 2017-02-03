using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.Parsing
{
    public enum ErrorCodes { NoError, WrongParameters, FileError, SyntaxError, SemanticError, CodeGenerationError, UnexpectedError }

    class ErrorListener : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Console.Error.WriteLine("Error parsing input file: {0} [line:{1}, column:{2}]", msg, line, charPositionInLine);
            Environment.ExitCode = (int)ErrorCodes.SyntaxError;
        }
    }
}

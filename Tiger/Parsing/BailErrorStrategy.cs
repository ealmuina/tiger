using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.Parsing
{
    class BailErrorStrategy : DefaultErrorStrategy
    {
        /** Instead of recovering from exception e, rethrow it */
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            throw e;
        }

        /** Make sure we don't attempt to recover inline; if the parser
         * successfully recovers, it won't throw an exception.
         */
        public override IToken RecoverInline(Parser recognizer)
        {
            throw new InputMismatchException(recognizer);
        }

        /** Make sure we don't attempt to recover from problems in subrules. */
        public override void Sync(Parser recognizer) { }
    }
}

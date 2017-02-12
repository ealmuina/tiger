using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Tiger.Parsing
{
    class BailTigerLexer : TigerLexer
    {
        public BailTigerLexer(ICharStream input) : base(input) { }

        public override void Recover(LexerNoViableAltException e)
        {
            throw e; //Bail out
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Tiger.AST;

namespace Tiger.Parsing
{
    class ASTBuilder : TigerBaseVisitor<Node>
    {
        public override Node VisitString([NotNull] TigerParser.StringContext context)
        {
            
        }
    }
}

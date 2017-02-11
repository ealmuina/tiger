using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class GeNode : ComparisonNode
    {
        public GeNode(ParserRuleContext context) : base(context) { }

        public override OpCode OperatorOpCode
        {
            get { return OpCodes.C; }
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            throw new NotImplementedException();
        }
    }
}

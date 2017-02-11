using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    abstract class Node
    {
        public Node(ParserRuleContext context)
        {
            Line = context.Start.Line;
            Column = context.Start.Column;
        }

        public Node(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; protected set; }

        public int Column { get; protected set; }

        public virtual string Type { get { return "None"; } }

        public List<Node> Children { get; protected set; } = new List<Node>();

        public abstract void CheckSemantics(Scope scope, List<SemanticError> errors);

        public abstract void Generate(CodeGenerator generator, SymbolTable symbols);
    }
}

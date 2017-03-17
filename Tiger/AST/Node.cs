using System.Collections.Generic;
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
            Column = context.Start.Column + 1;
        }

        public Node(int line, int column)
        {
            Line = line;
            Column = column + 1;
        }

        public int Line { get; }

        public int Column { get; }

        public virtual string Type => Types.Void;

        public List<Node> Children { get; } = new List<Node>();

        public abstract void CheckSemantics(Scope scope, List<SemanticError> errors);

        public abstract void Generate(CodeGenerator generator);
    }
}

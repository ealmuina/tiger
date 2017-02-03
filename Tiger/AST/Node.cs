using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Tiger.Semantics;

namespace Tiger.AST
{
    abstract class Node
    {
        public List<Node> Children { get; protected set; } = new List<Node>();

        public string Text { get; protected set; }

        public abstract void CheckSemantics(Scope scope, List<SemanticError> errors);

        public abstract void Generate(ILGenerator generator);
    }
}

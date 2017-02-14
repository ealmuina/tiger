using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.AST;

namespace Tiger.Semantics
{
    class SemanticError
    {
        public string Message { get; set; }

        public Node Node { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public static SemanticError InvalidUseOfOperator(string op, string type, string member, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Invalid use of {0} operator with a non-{1} {2} value", op, type, member),
                Node = node
            };
        }
    }
}

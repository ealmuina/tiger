using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class FuncDeclNode : DeclarationNode
    {
        public FuncDeclNode(ParserRuleContext context) : base(context) { }

        public string FunctionType
        {
            get
            {
                return Children[1] != null ?
                    (Children[1] as IdNode).Name : Types.Void;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            throw new NotImplementedException();
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            throw new NotImplementedException();
        }
    }
}

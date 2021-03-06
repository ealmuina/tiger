﻿using Antlr4.Runtime;
using System.Collections.Generic;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class AuxiliaryNode : Node
    {
        public AuxiliaryNode(ParserRuleContext context) : base(context) { }

        public AuxiliaryNode(int line, int column) : base(line, column) { }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }

        public override void Generate(CodeGenerator generator)
        {
            //pass
        }
    }
}

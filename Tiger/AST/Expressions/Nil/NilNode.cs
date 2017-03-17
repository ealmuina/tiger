﻿using Antlr4.Runtime;
using System.Collections.Generic;
using System.Reflection.Emit;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class NilNode : ExpressionNode
    {
        public NilNode() : base(-1, -1) { }

        public NilNode(ParserRuleContext context) : base(context) { }

        public override string Type => Types.Nil;

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            il.Emit(OpCodes.Ldnull);
        }
    }
}

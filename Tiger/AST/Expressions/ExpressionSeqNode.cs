﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class ExpressionSeqNode : ExpressionNode
    {
        public ExpressionSeqNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get
            {
                if (Children.Count > 0 && !Children.Exists(x => x is BreakNode))
                    return Children.Last().Type;
                return "None";
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var expr in Children)
                expr.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            foreach (var expr in Children)
            {
                expr.Generate(generator, symbols);
                if (expr != Children.Last() && expr.Type != "None")
                    generator.Generator.Emit(OpCodes.Pop);
            }
        }
    }
}

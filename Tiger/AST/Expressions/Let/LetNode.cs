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
    class LetNode : ExpressionNode
    {
        public LetNode(ParserRuleContext context) : base(context) { }

        public LetNode(int line, int column) : base(line, column) { }

        public override string Type
        {
            get
            {
                return Children.Count > 1 ?
                    Children.Last().Type : Types.Void;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            scope = (Scope)scope.Clone();
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            symbols = (SymbolTable)symbols.Clone();
            var il = generator.Generator;

            Children[0].Generate(generator, symbols);

            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].Generate(generator, symbols);
                if (i < Children.Count - 1 && Children[i].Type != Types.Void)
                    il.Emit(OpCodes.Pop);
            }
        }
    }
}

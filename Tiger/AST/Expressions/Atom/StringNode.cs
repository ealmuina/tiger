using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class StringNode : AtomNode
    {
        public StringNode(ParserRuleContext context, string text) : base(context)
        {
            //TODO Chequear si dejo el escape de /.../ aqui o si se puede pasar al lexer
            Text = "";
            bool escaped = false;
            for (int i = 1; i < text.Length - 1; i++)
            {
                if (text[i] == '\\')
                {
                    if (escaped) escaped = false;
                    else escaped = true;
                    continue;
                }
                if (escaped) continue;
                Text += text[i];
            }

            Type = "String";
        }

        public string Text { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            //pass
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            generator.Generator.Emit(OpCodes.Ldstr, Text);
        }
    }
}

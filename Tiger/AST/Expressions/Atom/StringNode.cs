﻿using System;
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
            Text = "";
            for (int i = 1; i < text.Length - 1; i++)
            {
                if (text[i] != '\\')
                {
                    Text += text[i];
                }
                else
                {
                    i++;
                    if (char.IsWhiteSpace(text[i]))
                    {
                        while (char.IsWhiteSpace(text[i]))
                            i++;
                    }
                    else if (char.IsDigit(text[i]))
                    {
                        var value = byte.Parse(text.Substring(i, 3));
                        Text += (char)value;
                        i += 2;
                    }
                    else
                    {
                        char c;
                        switch (text[i])
                        {
                            case 'n':
                                c = '\n';
                                break;
                            case 't':
                                c = '\t';
                                break;
                            case 'r':
                                c = '\r';
                                break;
                            case '\\':
                                c = '\\';
                                break;
                            default: //case '\"'
                                c = '\"';
                                break;
                        }
                        Text += c;
                    }
                }
            }
        }

        public string Text { get; protected set; }

        public override string Type
        {
            get { return Types.String; }
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            generator.Generator.Emit(OpCodes.Ldstr, Text);
        }
    }
}

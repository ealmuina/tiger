using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class StringNode : AtomNode
    {
        public StringNode(ParserRuleContext context, string text) : base(context)
        {
            Type = Types.String;

            Text = "";
            for (int i = 1; i < text.Length - 1; i++) // 0 to Length - 1 in order to remove the (Antlr delivered) ""
            {
                if (text[i] != '\\')
                {
                    Text += text[i]; // no problem if it isn't \\
                }
                else
                {
                    i++; // skip it and see whats next
                    if (char.IsWhiteSpace(text[i]))
                    {
                        // it's a \EMPTY*\. Just skip whitespaces
                        while (char.IsWhiteSpace(text[i]))
                            i++;
                    }
                    else if (char.IsDigit(text[i]))
                    {
                        // its an ASCII. Add the corresponding character
                        var value = byte.Parse(text.Substring(i, 3));
                        Text += (char)value;
                        i += 2;
                    }
                    else
                    {
                        // Escape sequence. Add the right one
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

        public string Text { get; }

        public override void Generate(CodeGenerator generator) => generator.Generator.Emit(OpCodes.Ldstr, Text);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;

namespace Tiger.AST
{
    class TypeFieldsNode : Node
    {
        public TypeFieldsNode(ParserRuleContext context, string[] names, string[] types) : base(context)
        {
            Names = names;
            Types = types;
        }

        public string[] Names { get; protected set; }

        public string[] Types { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (Names.GroupBy(n => n).Count() != Names.Length)
                errors.Add(new SemanticError
                {
                    Message = string.Format("At least two fields have the same name"),
                    Node = this
                });

            foreach (var type in Types)
                if (!scope.IsDefined<Semantics.TypeInfo>(type))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Undefined parameter type '{0}'", type),
                        Node = this
                    });
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            //pass
        }
    }
}

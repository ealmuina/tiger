using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;

namespace Tiger.AST
{
    class RecordTypeNode : TypeNode
    {
        public RecordTypeNode(ParserRuleContext context, string[] names, string[] types) : base(context)
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
                    Message = $"At least two fields have the same name",
                    Node = this
                });

            foreach (var type in Types)
                if (!scope.IsDefined<Semantics.TypeInfo>(type))
                    errors.Add(new SemanticError
                    {
                        Message = $"Undefined field type '{type}'",
                        Node = this
                    });
        }

        public Dictionary<string, FieldBuilder> Define(CodeGenerator generator)
        {
            var result = new Dictionary<string, FieldBuilder>();
            for (int i = 0; i < Names.Length; i++)
                result[Names[i]] = generator.Type.DefineField(Names[i], generator.Types[Types[i]], FieldAttributes.Public);
            return result;
        }

        public override void Generate(CodeGenerator generator)
        {
            //pass
        }
    }
}

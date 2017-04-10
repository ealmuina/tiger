using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;

namespace Tiger.AST
{
    class FieldsListNode : TypeNode
    {
        public FieldsListNode(ParserRuleContext context, string[] names, string[] types) : base(context)
        {
            Names = names;
            TypesNames = types;
        }

        public string[] Names { get; }

        public string[] TypesNames { get; }

        public Semantics.TypeInfo[] Types { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            if (Names.GroupBy(n => n).Count() != Names.Length)
                errors.Add(new SemanticError
                {
                    Message = "At least two fields have the same name",
                    Node = this
                });

            foreach (var type in TypesNames)
                if (!scope.IsDefined<Semantics.TypeInfo>(type))
                    errors.Add(new SemanticError
                    {
                        Message = $"Undefined field type '{type}'",
                        Node = this
                    });

            if (errors.Any()) return;

            Types = TypesNames.Select(t => scope.GetItem<Semantics.TypeInfo>(t)).ToArray();
        }

        /// <summary>
        /// Define the fields as members of generator.Type
        /// </summary>
        /// <returns>Dictionary that associates a name of a field with its corresponding FieldBuilder</returns>
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

using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class FieldAccessNode : LValueNode
    {
        string type;

        public FieldAccessNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return type; }
        }

        public string FieldName
        {
            get { return (Children[1] as IdNode).Name; }
        }

        public TypeInfo ContainerType { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            TypeInfo info = scope.DefinedTypes[Children[0].Type];

            if (!info.FieldNames.Contains(FieldName))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Type '{0}' doesn't have a field named '{1}'", info.Name, FieldName),
                    Node = Children[1]
                });
            else
                type = info.FieldTypes[Array.IndexOf(info.FieldNames, FieldName)];

            ContainerType = info;
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            FieldBuilder field = generator.Fields[ContainerType.Name][FieldName];

            Children[0].Generate(generator);

            if (ByValue)
                il.Emit(OpCodes.Ldfld, field);
            else
                il.Emit(OpCodes.Ldflda, field);
        }
    }
}

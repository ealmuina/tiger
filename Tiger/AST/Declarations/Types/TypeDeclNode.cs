﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;

namespace Tiger.AST
{
    class TypeDeclNode : DeclarationNode
    {
        CodeGenerator generator;

        public TypeDeclNode(ParserRuleContext context) : base(context) { }

        public bool IsAlias
        {
            get { return Children[1] is IdNode; }
        }

        public Semantics.TypeInfo TypeInfo { get; protected set; }

        public void DefineType(Scope scope, List<SemanticError> errors)
        {
            if (IsAlias)
            {
                var alias = (IdNode)Children[1];
                scope.DefineType(Name, alias.Name);
            }
            else if (Children[1] is RecordTypeNode)
            {
                var record = (RecordTypeNode)Children[1];
                scope.DefineType(Name, record.Names, record.Types);
            }
            else
            {
                //TODO Implement
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (IsAlias && scope.BadAlias(Name))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Type '{0}' is part of an invalid alias cycle", Name),
                    Node = this
                });
            else
                TypeInfo = scope.GetItem<Semantics.TypeInfo>(Name);
        }

        public void Define(CodeGenerator generator)
        {
            if (IsAlias)
                return;

            if (Children[1] is RecordTypeNode)
            {
                this.generator = (CodeGenerator)generator.Clone();
                this.generator.Type = this.generator.Module.DefineType(Name, TypeAttributes.Public);
                this.generator.Type.DefineDefaultConstructor(MethodAttributes.Public);
                this.generator.Types[Name] = this.generator.Type;
                Dictionary<string, FieldBuilder> fields = (Children[1] as RecordTypeNode).Define(this.generator);

                generator.Types[Name] = this.generator.Type.CreateType();
                generator.Fields[Name] = fields;
            }

            else //Children[1] is ArrayTypeNode
            {

            }
        }



        public override void Generate(CodeGenerator generator)
        {
            //pass
        }
    }
}

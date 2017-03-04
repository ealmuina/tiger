using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;

namespace Tiger.AST
{
    class VarDeclNode : DeclarationNode
    {
        public VarDeclNode(ParserRuleContext context, bool readOnly = false) : base(context)
        {
            IsReadonly = readOnly;
        }

        public VarDeclNode(int line, int column, bool readOnly = false) : base(line, column)
        {
            IsReadonly = readOnly;
        }

        public bool IsReadonly { get; protected set; }

        public override string Type
        {
            get
            {
                return Children[1] != null ?
                    (Children[1] as IdNode).Name : Children[2].Type;
            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children.Where(n => n != null))
                node.CheckSemantics(scope, errors);

            if (Children[2].Type == Types.Void)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Expression assigned to variable does not return a value"),
                    Node = this
                });

            if (Children[1] == null && Children[2].Type == Types.Nil)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Variable type cannot be infered from an expression which returns nil"),
                    Node = this
                });

            if (Children[1] != null && !scope.Types.ContainsKey(Type))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Cannot declare variable of undefined type '{0}'", Type),
                    Node = Children[1]
                });

            else if (Children[1] != null && Children[2].Type != Types.Nil && !scope.SameType((Children[1] as IdNode).Name, Children[2].Type))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Expression assigned to variable does not match with its type"),
                    Node = this
                });

            if (errors.Count == 0)
                scope.DefineVariable(Name, scope.GetItem<Semantics.TypeInfo>(Type).Name, IsReadonly, false);
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            Type type = generator.GetType(Type);

            Children[2].Generate(generator);

            LocalBuilder variable = il.DeclareLocal(type);
            il.Emit(OpCodes.Stloc, variable);
            generator.Variables[Name] = variable;
        }
    }
}

using System;
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

        public string VariableType
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

            scope.DefineVariable(Name, VariableType, IsReadonly, false);

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

            if (Children[1] != null && Children[2].Type != Types.Nil && !scope.SameType((Children[1] as IdNode).Name, Children[2].Type))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Expression assigned to variable does not match with its type"),
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            Type type = generator.GetType(VariableType);
            LocalBuilder variable = il.DeclareLocal(type);

            Children[2].Generate(generator);
            il.Emit(OpCodes.Stloc, variable);

            generator.Variables[Name] = variable;
        }
    }
}

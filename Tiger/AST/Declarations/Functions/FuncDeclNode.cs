using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection.Emit;
using System.Reflection;

namespace Tiger.AST
{
    class FuncDeclNode : DeclarationNode
    {
        CodeGenerator generator;
        VariableInfo[] foreignVariables;

        public FuncDeclNode(ParserRuleContext context) : base(context) { }

        public RecordTypeNode Arguments
        {
            get
            {
                return Children[1] != null ?
                    (RecordTypeNode)Children[1] : null;
            }
        }

        public string FunctionType
        {
            get
            {
                return Children[2] != null ?
                    (Children[2] as IdNode).Name : Types.Void;
            }
        }

        public ExpressionNode Expression
        {
            get { return (ExpressionNode)Children[3]; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreignVariables = (VariableInfo[])scope.Variables.Clone();
            scope = new Scope(scope);
            scope.InsideLoop = false;
            var info = (FunctionInfo)scope[Name];

            foreach (var fv in foreignVariables)
            {
                scope.DefineVariable(fv.Name, fv.Type, (scope[fv.Name] as VariableInfo).IsReadOnly, true);
                info.ForeignVars.Add(fv.Name);
            }

            if (scope.Stdl.Where(n => n.Name == Name).Count() > 0)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Standard library function '{0}' cannot be redefined", Name),
                    Node = this
                });

            if (Arguments != null)
            {
                Arguments.CheckSemantics(scope, errors);

                string[] names = Arguments.Names;
                string[] types = Arguments.Types;

                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == Name)
                        errors.Add(new SemanticError
                        {
                            Message = string.Format("There is an argument named as the function"),
                            Node = this
                        });

                    scope.DefineVariable(names[i], types[i], false, false);
                    info.ForeignVars.Remove(names[i]);
                }
            }

            Expression.CheckSemantics(scope, errors);

            if (Expression.Type != Types.Nil && FunctionType != Expression.Type)
                errors.Add(new SemanticError
                {
                    Message = string.Format("The expression assigned to function '{0}' returns '{1}' and doesn't match with the expected '{2}'",
                                            Name, Expression.Type, FunctionType),
                    Node = this
                });
        }

        public void Define(CodeGenerator generator)
        {
            MethodBuilder func = generator.Type.DefineMethod(Name, MethodAttributes.Static);
            func.SetReturnType(generator.Types[FunctionType]);
            generator.Functions[Name] = func;

            this.generator = new CodeGenerator(generator);
            this.generator.Functions = generator.Functions;
            generator = this.generator;
            generator.Method = func;
            generator.Generator = func.GetILGenerator();
            generator.Variables.Clear();

            var paramTypes = new List<Type>();

            //Store parameters types
            if (Arguments != null)
            {
                string[] names = Arguments.Names;
                string[] types = Arguments.Types;

                for (int i = 0; i < names.Length; i++)
                {
                    generator.ParamIndex[names[i]] = i;
                    paramTypes.Add(generator.Types[types[i]]);
                }
            }

            //Store foreign variables types to pass them as ref parameters
            VariableInfo[] fv = foreignVariables;
            int paramIndex = Arguments != null ? Arguments.Names.Length : 0;
            for (int i = 0; i < fv.Length; i++)
            {
                if (Arguments != null && Arguments.Names.Contains(fv[i].Name))
                    continue;

                generator.ParamIndex[fv[i].Name] = paramIndex++;
                paramTypes.Add(generator.Types[fv[i].Type].MakeByRefType());
            }

            //Set parameters types
            generator.Method.SetParameters(paramTypes.ToArray());

            //Set parameters names
            if (Arguments != null)
            {
                string[] names = Arguments.Names;

                for (int i = 0; i < names.Length; i++)
                    generator.Method.DefineParameter(i + 1, ParameterAttributes.None, names[i]);
            }

            //Set names of parameters representing foreign variables 
            paramIndex = Arguments != null ? Arguments.Names.Length + 1 : 1;
            for (int i = 0; i < fv.Length; i++)
            {
                if (Arguments != null && Arguments.Names.Contains(fv[i].Name))
                    continue;

                generator.Method.DefineParameter(paramIndex++, ParameterAttributes.None, fv[i].Name);
            }
        }

        public override void Generate(CodeGenerator generator)
        {
            generator = this.generator;
            ILGenerator il = generator.Generator;

            // Save arguments to local variables
            if (Arguments != null)
            {
                string[] names = Arguments.Names;
                string[] types = Arguments.Types;

                for (int i = 0; i < names.Length; i++)
                {
                    LocalBuilder arg = generator.Generator.DeclareLocal(generator.Types[types[i]]);
                    generator.Variables[names[i]] = arg;

                    il.Emit(OpCodes.Ldarg, i);
                    il.Emit(OpCodes.Stloc, arg);
                }
            }

            Expression.Generate(generator);
            il.Emit(OpCodes.Ret);
        }
    }
}

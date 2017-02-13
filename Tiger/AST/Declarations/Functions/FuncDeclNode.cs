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
    class FuncDeclNode : DeclarationNode
    {
        public FuncDeclNode(ParserRuleContext context) : base(context) { }

        public TypeFieldsNode Arguments
        {
            get
            {
                return Children[1] != null ?
                    (TypeFieldsNode)Children[1] : null;
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
            scope.DefineFunction(Name, FunctionType, Arguments.Types);
            scope = (Scope)scope.Clone();

            if (Arguments != null)
            {
                Arguments.CheckSemantics(scope, errors);

                string[] names = Arguments.Names;
                string[] types = Arguments.Types;

                for (int i = 0; i < names.Length; i++)
                    scope.DefineVariable(names[i], types[i], false, true);
            }

            Expression.CheckSemantics(scope, errors);
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            ILGenerator currentil = generator.Generator; //store current generator

            MethodBuilder func = generator.Type.DefineMethod(Name, MethodAttributes.Static);
            func.SetReturnType(symbols.Types[FunctionType]);
            symbols.Functions[Name] = func;

            generator.Method = func;
            generator.Generator = func.GetILGenerator();

            symbols = (SymbolTable)symbols.Clone();

            if (Arguments != null)
            {
                string[] names = Arguments.Names;
                string[] types = Arguments.Types;
                var paramTypes = new List<Type>();

                for (int i = 0; i < names.Length; i++)
                {
                    symbols.ParamIndex[names[i]] = i;
                    paramTypes.Add(symbols.Types[types[i]]);
                }

                func.SetParameters(paramTypes.ToArray());
            }

            Expression.Generate(generator, symbols);

            if (FunctionType != Types.Void)
                generator.Generator.Emit(OpCodes.Ret);

            generator.Generator = currentil; //back to old generator
        }
    }
}

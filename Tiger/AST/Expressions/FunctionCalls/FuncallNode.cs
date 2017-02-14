using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;
using System.Linq.Expressions;
using System.Reflection;

namespace Tiger.AST
{
    class FuncallNode : ExpressionNode
    {
        public FuncallNode(ParserRuleContext context) : base(context) { }

        public string FunctionName
        {
            get { return (Children[0] as IdNode).Name; }
        }

        public IEnumerable<ExpressionNode> Arguments
        {
            get { return Children.Skip(1).Cast<ExpressionNode>(); }
        }

        public FunctionInfo SymbolInfo { get; private set; }

        public override string Type
        {
            get { return SymbolInfo.Type; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var arg in Arguments)
                arg.CheckSemantics(scope, errors);

            if (!scope.IsDefined(FunctionName))
            {
                errors.Add(new SemanticError
                {
                    Message = string.Format("Function '{0}' does not exist", FunctionName),
                    Node = this
                });
                return;
            }

            ItemInfo info = scope[FunctionName];
            if (!(info is FunctionInfo))
            {
                errors.Add(new SemanticError
                {
                    Message = string.Format("Variable or constant '{0}' is being used as a function", FunctionName),
                    Node = this
                });
                return;
            }

            var fInfo = (FunctionInfo)info;
            int parameterCount = fInfo.Parameters.Length;
            int argumentCount = Arguments.Count();
            if (parameterCount != argumentCount)
            {
                errors.Add(new SemanticError
                {
                    Message = string.Format("Function '{0}' takes {1} arguments, got {2} instead", FunctionName, parameterCount, argumentCount),
                    Node = this
                });
                return;
            }

            var arguments = Arguments.ToArray();
            for (int i = 0; i < parameterCount; i++)
            {
                string expectedT = fInfo.Parameters[i];
                string exprT = arguments[i].Type;

                if (exprT != expectedT)
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Called function {0} with argument type '{1}' when expecting '{2}'", FunctionName, exprT, expectedT),
                        Node = arguments[i]
                    });
            }

            SymbolInfo = fInfo;
        }

        public override void Generate(CodeGenerator generator)
        {
            foreach (var arg in Arguments)
                arg.Generate(generator);
            generator.Generator.Emit(OpCodes.Call, generator.Functions[SymbolInfo.Name]);
        }
    }
}

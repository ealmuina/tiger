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

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var arg in Arguments)
                arg.CheckSemantics(scope, errors);

            if (!scope.IsDefined(FunctionName))
            {
                errors.Add(SemanticError.FunctionDoesNotExist(FunctionName, this));
                return;
            }

            ItemInfo info = scope[FunctionName];
            if (!(info is FunctionInfo))
            {
                errors.Add(SemanticError.VariableOrConstantUsedAsFunction(FunctionName, this));
                return;
            }

            var fInfo = (FunctionInfo)info;
            //int parameterCount = fInfo.ParameterCount;
            //int argumentCount = Arguments.Count();
            //if (parameterCount != argumentCount)
            //{
            //    errors.Add(SemanticError.WrongParameterNumber(FunctionName, parameterCount, argumentCount, this));
            //    return;
            //}

            SymbolInfo = fInfo;
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            foreach (var arg in Arguments)
                arg.Generate(generator, symbols);
            generator.Generator.Emit(OpCodes.Call, symbols.Functions[SymbolInfo.Name]);
        }
    }
}

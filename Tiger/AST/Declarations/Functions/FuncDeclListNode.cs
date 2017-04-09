using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System;

namespace Tiger.AST
{
    class FuncDeclListNode : Node, IDeclarationList
    {
        public FuncDeclListNode(ParserRuleContext context) : base(context) { }

        public string[] DeclaredNames
        {
            get =>(from f in Children.Cast<FuncDeclNode>() select f.Name).ToArray();
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var functions = Children.Cast<FuncDeclNode>().ToList();

            foreach (var func in functions)
            {                
                if (!scope.IsDefined<TypeInfo>(func.Type)) 
                {                    
                    errors.Add(new SemanticError
                    {
                        Message = $"Function '{func.Name}' return type '{func.Type}' is undefined in its scope",
                        Node = this
                    });
                    return;
                } // In order to be able to define correctly the function below we need to check here that its return type is correct

                scope.DefineFunction(func.Name, func.Type,
                    func.Arguments != null ? func.Arguments.Types : new string[] { });
            }

            functions.ForEach(f => f.CheckSemantics(scope, errors));
        }

        public override void Generate(CodeGenerator generator)
        {
            var functions = Children.Cast<FuncDeclNode>().ToList();

            functions.ForEach(f => f.Define(generator));
            functions.ForEach(f => f.Generate(generator));
        }
    }
}

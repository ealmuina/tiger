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
            get => (from f in Children.Cast<FuncDeclNode>() select f.Name).ToArray();
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var functions = Children.Cast<FuncDeclNode>().ToList();

            functions.ForEach(f => f.DefineFunction(scope, errors));

            if (errors.Count == 0)
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

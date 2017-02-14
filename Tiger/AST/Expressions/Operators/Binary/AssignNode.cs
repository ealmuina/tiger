using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Tiger.Semantics;
using Tiger.CodeGeneration;

namespace Tiger.AST
{
    class AssignNode : ExpressionNode
    {
        public AssignNode(ParserRuleContext context) : base(context) { }

        public AssignNode(int line, int column): base(line, column) { }

        public LValueNode LValue
        {
            get { return Children[0] as LValueNode; }
        }

        public ExpressionNode Expression
        {
            get { return Children[1] as ExpressionNode; }
        }

        public VariableInfo SymbolInfo { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (Expression.Type == Types.Void)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Expression being assigned does not return a value"),
                    Node = Expression
                });

            else if (LValue.Type != Expression.Type && Expression.Type != Types.Nil)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Incompatible types for assignation"),
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            Expression.Generate(generator);
            LValue.Generate(generator);   
        }
    }
}

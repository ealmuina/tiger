using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class IfNode : FlowControlNode
    {
        public IfNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get
            {
                return Children[2] == null ? "None" : Children[2].Type;

            }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var expr in Children.Where(e => e != null))
                expr.CheckSemantics(scope, errors);

            if (Children[0].Type != "Int")
                errors.Add(SemanticError.IfConditionNotInteger(Children[0]));

            if (Children[2] == null && Children[1].Type != "None") //if-then
                errors.Add(SemanticError.IfThenNotReturns(Children[1]));

            if (Children[2] != null && Children[1].Type != Children[2].Type) //if-then-else
                errors.Add(SemanticError.IfThenElseBadTypes(this));
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            Children[0].Generate(generator, symbols);

            ILGenerator il = generator.Generator;
            Label _else = il.DefineLabel();
            Label end = il.DefineLabel();

            // Check <if expression>
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, _else);

            //true
            Children[1].Generate(generator, symbols);
            il.Emit(OpCodes.Br, end); //skip false expr

            //false
            il.MarkLabel(_else);
            if (Children[2] != null)
                Children[2].Generate(generator, symbols);

            il.MarkLabel(end);
        }
    }
}

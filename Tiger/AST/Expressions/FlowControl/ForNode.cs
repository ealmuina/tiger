using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class ForNode : FlowControlNode
    {
        public ForNode(ParserRuleContext context) : base(context) { }

        public string Cursor
        {
            get { return (Children[0] as VarDeclNode).Name; }
        }

        public override string Type
        {
            get { return Children[2].Type; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            var clone = (Scope)scope.Clone();

            Children[0].CheckSemantics(clone, errors);
            Children[1].CheckSemantics(scope, errors);
            Children[2].CheckSemantics(clone, errors);

            if ((Children[0] as VarDeclNode).VariableType != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = string.Format("The return type of the expression for the lower bound of the for loop is not integer"),
                    Node = Children[0]
                });

            if (Children[1].Type != Types.Int)
                errors.Add(new SemanticError
                {
                    Message = string.Format("The expression for the upper bound of the for loop does not return a value"),
                    Node = Children[1]
                });
        }

        public override void Generate(CodeGenerator generator, SymbolTable symbols)
        {
            symbols = (SymbolTable)symbols.Clone();
            ILGenerator il = generator.Generator;

            Children[0].Generate(generator, symbols);
            Children[1].Generate(generator, symbols);
            LocalBuilder top = il.DeclareLocal(typeof(int));
            il.Emit(OpCodes.Stloc, top);
            LocalBuilder cursor = symbols.Variables[Cursor];

            Label condition = il.DefineLabel();
            Label end = il.DefineLabel();

            Label loopEnd = symbols.LoopEnd; //store current loopEnd so we can restore it later
            symbols.LoopEnd = end;

            if (Type != Types.Void)
                il.Emit(OpCodes.Ldc_I4_0); //dummy value on evaluation stack

            //Check if upper bound was reached
            il.MarkLabel(condition);
            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldloc, top);
            il.Emit(OpCodes.Bgt, end);

            //For body
            if (Type != Types.Void)
                il.Emit(OpCodes.Pop); //forget last iteration value
            Children[2].Generate(generator, symbols);

            //Increase cursor value and continue iteration
            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, cursor);
            il.Emit(OpCodes.Br, condition);

            //end
            il.MarkLabel(end);

            symbols.LoopEnd = loopEnd;
        }
    }
}

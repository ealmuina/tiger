using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Tiger.CodeGeneration;
using Tiger.Semantics;

namespace Tiger.AST
{
    class ArrayNode : ExpressionNode
    {
        public ArrayNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get => (Children[0] as IdNode).Name;
        }

        public ExpressionNode SizeExpr
        {
            get => Children[1] as ExpressionNode;
        }

        public ExpressionNode InitExpr
        {
            get => Children[2] as ExpressionNode;
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            Children.ForEach(n => n.CheckSemantics(scope, errors));

            if (errors.Count > 0) return;

            if (!scope.IsDefined<TypeInfo>(Type) || !(scope.GetItem<TypeInfo>(Type) is ArrayInfo info))
                errors.Add(new SemanticError
                {
                    Message = $"Undefined array type '{Type}'",
                    Node = this
                });
            else
            {
                if (!scope.SameType(InitExpr.Type, info.ElementsType))
                    errors.Add(new SemanticError
                    {
                        Message = $"Array elements initial value type is '{InitExpr.Type}' which isn't an alias for expected '{info.ElementsType}'",
                        Node = this
                    });
            }

            if (!scope.SameType(SizeExpr.Type, Types.Int))
                errors.Add(new SemanticError
                {
                    Message = $"Array size expression type is '{SizeExpr.Type}' which isn't an alias for 'Int'",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            Label loop = il.DefineLabel();
            Label end = il.DefineLabel();

            LocalBuilder cursor = il.DeclareLocal(generator.Types[Types.Int]);
            LocalBuilder size = il.DeclareLocal(generator.Types[Types.Int]);
            LocalBuilder array = il.DeclareLocal(generator.Types[Type]);

            Type type = generator.Types[Type].GetElementType();

            SizeExpr.Generate(generator);
            il.Emit(OpCodes.Stloc, size);   // store size

            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Newarr, type);
            il.Emit(OpCodes.Stloc, array);  // store array

            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc, cursor); // store cursor = 0

            il.MarkLabel(loop);
            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Bge, end);      // End if cursor >= size

            il.Emit(OpCodes.Ldloc, array);
            il.Emit(OpCodes.Ldloc, cursor);
            InitExpr.Generate(generator);
            il.Emit(OpCodes.Stelem, type);  // array[cursor] = InitExpr result

            il.Emit(OpCodes.Ldloc, cursor);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, cursor); // cursor++
            il.Emit(OpCodes.Br, loop);      // keep iterating

            il.MarkLabel(end);
            il.Emit(OpCodes.Ldloc, array);
        }
    }
}

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
            get { return (Children[0] as IdNode).Name; }
        }

        public ExpressionNode SizeExpr
        {
            get { return Children[1] as ExpressionNode; }
        }

        public ExpressionNode InitExpr
        {
            get { return Children[2] as ExpressionNode; }
        }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            if (errors.Count > 0) return;

            if (!scope.IsDefined<TypeInfo>(Type) || !(scope.GetItem<TypeInfo>(Type) is ArrayInfo))
                errors.Add(new SemanticError
                {
                    Message = $"Undefined array type '{Type}'",
                    Node = this
                });
            else
            {
                var info = (ArrayInfo)scope.GetItem<TypeInfo>(Type);
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

            LocalBuilder index = il.DeclareLocal(generator.Types[Types.Int]);
            LocalBuilder size = il.DeclareLocal(generator.Types[Types.Int]);
            LocalBuilder array = il.DeclareLocal(generator.Types[Type]);

            Type type = generator.Types[Type].GetElementType();

            SizeExpr.Generate(generator);
            il.Emit(OpCodes.Stloc, size);

            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Newarr, type);
            il.Emit(OpCodes.Stloc, array);

            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc, index);

            il.MarkLabel(loop);
            il.Emit(OpCodes.Ldloc, index);
            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Bge, end);

            il.Emit(OpCodes.Ldloc, array);
            il.Emit(OpCodes.Ldloc, index);
            InitExpr.Generate(generator);
            il.Emit(OpCodes.Stelem, type);

            il.Emit(OpCodes.Ldloc, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, index);
            il.Emit(OpCodes.Br, loop);

            il.MarkLabel(end);
            il.Emit(OpCodes.Ldloc, array);
        }
    }
}

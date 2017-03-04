using Antlr4.Runtime;
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

            if (!scope.IsDefined<TypeInfo>(Type) || !scope.IsArrayType(Type))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Undefined array type '{0}'", Type),
                    Node = this
                });
            else
            {
                var info = scope.GetItem<TypeInfo>(Type);
                if (!scope.SameType(InitExpr.Type, info.Name))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Array elements initial value type is '{0}' which isn't an alias for expected '{1}'",
                                                InitExpr.Type, info.Name),
                        Node = this
                    });
            }

            if (!scope.SameType(SizeExpr.Type, Types.Int))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Array size expression type is '{0}' which isn't an alias for 'Int'", SizeExpr.Type),
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;

            Label loop = il.DefineLabel();
            Label end = il.DefineLabel();

            LocalBuilder size = il.DeclareLocal(generator.Types[Types.Int]);
            LocalBuilder initVal = il.DeclareLocal(generator.Types[InitExpr.Type]);
            LocalBuilder array = il.DeclareLocal(generator.Types[Type]);

            SizeExpr.Generate(generator);
            il.Emit(OpCodes.Stloc, size);
            InitExpr.Generate(generator);
            il.Emit(OpCodes.Stloc, initVal);

            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Newarr, generator.Types[InitExpr.Type]);
            il.Emit(OpCodes.Stloc, array);

            il.MarkLabel(loop);
            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, size);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Blt, end);

            il.Emit(OpCodes.Ldloc, array);
            il.Emit(OpCodes.Ldloc, size);
            il.Emit(OpCodes.Ldloc, initVal);
            il.Emit(OpCodes.Stelem, generator.Types[InitExpr.Type]);
            il.Emit(OpCodes.Br, loop);

            il.MarkLabel(end);
            il.Emit(OpCodes.Ldloc, array);
        }
    }
}

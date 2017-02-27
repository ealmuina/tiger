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

        public TypeInfo Info { get; protected set; }

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            TypeInfo info;
            if (!scope.Types.TryGetValue(Type, out info) || !info.IsArray)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Undefined array type '{0}'", Type),
                    Node = this
                });
            else
            {
                if (!scope.SameType(InitExpr.Type, info.FieldTypes[0]))
                    errors.Add(new SemanticError
                    {
                        Message = string.Format("Array elements initial value type is '{0}' which isn't an alias for expected '{1}'",
                                                InitExpr.Type, info.FieldTypes[0]),
                        Node = this
                    });
            }

            if (!scope.SameType(SizeExpr.Type, Types.Int))
                errors.Add(new SemanticError
                {
                    Message = string.Format("Array size expression type is '{0}' which isn't an alias for 'Int'", SizeExpr.Type),
                    Node = this
                });

            foreach (var node in Children)
                node.CheckSemantics(scope, errors);

            Info = info;
        }

        public override void Generate(CodeGenerator generator)
        {
            ILGenerator il = generator.Generator;
            Label loop = il.DefineLabel();
            Label end = il.DefineLabel();
            LocalBuilder initExpr = il.DeclareLocal(generator.Types[InitExpr.Type]);
            LocalBuilder array = il.DeclareLocal(generator.Types[Type]);

            SizeExpr.Generate(generator);
            InitExpr.Generate(generator);
            il.Emit(OpCodes.Stloc, initExpr);

            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Newarr, generator.Types[Type]);
            il.Emit(OpCodes.Stloc, array);

            il.MarkLabel(loop);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Blt, end);

            il.

            for (int i = 1; i < Children.Count; i++)
            {
                var field = (FieldNode)Children[i];
                il.Emit(OpCodes.Dup);
                field.Generate(generator);
                il.Emit(OpCodes.Stfld, generator.Fields[Type][field.Name]);
            }
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ldloc, record);
        }
    }
}

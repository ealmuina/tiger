﻿using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Tiger.CodeGeneration;
using Tiger.Semantics;
using System.Reflection;
using System.Linq;

namespace Tiger.AST
{
    abstract class ComparisonNode : BinaryNode
    {
        public ComparisonNode(ParserRuleContext context) : base(context)
        {
            Type = Types.Int;
        }

        protected abstract bool SupportType(Semantics.TypeInfo type);

        protected abstract void CompareInt(ILGenerator il);

        protected virtual void CompareString(ILGenerator il)
        {
            //Compare the strings using the string's CompareTo method
            MethodInfo compareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
            il.Emit(OpCodes.Call, compareTo);
        }

        protected abstract void CompareOther(ILGenerator il);

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (errors.Any()) return;

            if (!SupportType(LeftOperand.Type))
                errors.Add(SemanticError.InvalidUseOfOperator("binary relational", "valid", "left", LeftOperand));

            if (!SupportType(RightOperand.Type))
                errors.Add(SemanticError.InvalidUseOfOperator("binary relational", "valid", "right", RightOperand));

            if (RightOperand.Type != LeftOperand.Type)
                errors.Add(new SemanticError
                {
                    Message = "Types of left and right operands of the binary relational operator do not match",
                    Node = this
                });

            if (LeftOperand.Type.Equals(Types.Nil) && RightOperand.Type.Equals(Types.Nil))
                errors.Add(new SemanticError
                {
                    Message = $"Types of left and right operands of the binary relational operator can't be both '{Types.Nil}'",
                    Node = this
                });

            if (LeftOperand is ComparisonNode || RightOperand is ComparisonNode)
                errors.Add(new SemanticError
                {
                    Message = "Comparison operators do not associate",
                    Node = this
                });
        }

        public override void Generate(CodeGenerator generator)
        {
            LeftOperand.Generate(generator);
            RightOperand.Generate(generator);

            ILGenerator il = generator.Generator;

            if (LeftOperand.Type == Types.Int)
                CompareInt(generator.Generator);

            else if (LeftOperand.Type == Types.String)
                CompareString(generator.Generator);

            else
                CompareOther(generator.Generator);
        }
    }
}

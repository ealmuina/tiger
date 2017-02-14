﻿using System;
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
    abstract class ComparisonNode : BinaryNode
    {
        public ComparisonNode(ParserRuleContext context) : base(context) { }

        public override string Type
        {
            get { return Types.Int; }
        }

        protected abstract bool SupportType(string type);

        public override void CheckSemantics(Scope scope, List<SemanticError> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            if (!SupportType(LeftOperand.Type))
                errors.Add(SemanticError.InvalidUseOfOperator("binary relational", "valid", "left", LeftOperand));

            if (!SupportType(RightOperand.Type))
                errors.Add(SemanticError.InvalidUseOfOperator("binary relational", "valid", "right", RightOperand));

            if (!scope.SameType(RightOperand.Type, LeftOperand.Type) && LeftOperand.Type != Types.Nil && RightOperand.Type != Types.Nil)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Types of left and right operands of the binary relational operator do not match"),
                    Node = this
                });

            if (LeftOperand is ComparisonNode || RightOperand is ComparisonNode)
                errors.Add(new SemanticError
                {
                    Message = string.Format("Comparison operators do not associate"),
                    Node = this
                });
        }
    }
}

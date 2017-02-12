using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.AST;

namespace Tiger.Semantics
{
    class SemanticError
    {
        public string Message { get; set; }

        public Node Node { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public static SemanticError InvalidNumber(string literal, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("'{0}' is not a valid number.", literal),
                Node = node
            };
        }

        public static SemanticError UndefinedVariableUsed(string name, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Variable '{0}' does not exist.", name),
                Node = node
            };
        }

        public static SemanticError FunctionUsedAsVariable(string name, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Function '{0}' used as variable or constant.", name),
                Node = node
            };
        }

        public static SemanticError FunctionDoesNotExist(string name, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Function '{0}' does not exist", name),
                Node = node
            };
        }

        public static SemanticError VariableOrConstantUsedAsFunction(string name, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Variable or constant '{0}' is being used as a function", name),
                Node = node
            };
        }

        public static SemanticError FunctionOrConstantUsedAsVariable(string name, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Can not assign value to function or constant '{0}'", name),
                Node = node
            };
        }

        public static SemanticError WrongParameterNumber(string name, int formalCount, int actualCount, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Function '{0}' takes {1} arguments, got {2} instead", formalCount, actualCount),
                Node = node
            };
        }

        public static SemanticError IncorrectTypeAssignation(string exprT, string expectedT, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("The expression return type '{0}' does not match with the expected type '{1}'", exprT, expectedT),
                Node = node
            };
        }

        public static SemanticError InvalidUseOfOperator(string op, string type, string member, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Invalid use of {0} operator with a non-{1} {2} value", op, type, member),
                Node = node
            };
        }

        public static SemanticError TypesDoNotMatch(string op, Node node)
        {
            return new SemanticError
            {
                Message = string.Format("Types of left and right operands of the binary {0} operator do not match", op),
                Node = node
            };
        }
    }
}

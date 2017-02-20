using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger.Semantics
{
    abstract class ItemInfo
    {
        protected ItemInfo(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; protected set; }

        public string Type { get; protected set; }
    }

    class VariableInfo : ItemInfo
    {
        public VariableInfo(string name, string type, bool readOnly, bool isForeign) : base(name, type)
        {
            IsReadOnly = readOnly;
            IsForeign = isForeign;
        }

        public bool IsReadOnly { get; protected set; }

        public bool IsForeign { get; protected set; }
    }

    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name, bool inStdl, string returnType, params string[] parameters)
            : base(name, returnType)
        {
            IsStdlFunc = inStdl;
            Parameters = parameters;
        }

        public string[] Parameters { get; protected set; }

        public bool IsStdlFunc { get; protected set; }

        public List<string> ForeignVars { get; } = new List<string>(); //stores the names of the foreign variables visibles from the function
    }

    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }

        public TypeInfo(string name, string[] fieldNames, string[] fieldTypes) : base(name, Types.Void)
        {
            FieldNames = fieldNames;
            FieldTypes = fieldTypes;
        }

        public string[] FieldNames { get; protected set; }

        public string[] FieldTypes { get; protected set; }
    }
}

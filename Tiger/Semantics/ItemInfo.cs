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
        public VariableInfo(string name, string type, bool readOnly, bool isparam) : base(name, type)
        {
            IsReadOnly = readOnly;
            IsParam = isparam;
        }

        public bool IsReadOnly { get; protected set; }

        public bool IsParam { get; protected set; }
    }

    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name, string returnType, params string[] parameters)
            : base(name, returnType)
        {
            Parameters = parameters;
        }

        public string[] Parameters { get; protected set; }
    }

    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }
    }
}

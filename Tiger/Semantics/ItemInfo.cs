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
        protected ItemInfo(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class VariableInfo : ItemInfo
    {
        public VariableInfo(string name) : base(name) { }

        public LocalBuilder LocalVariable { get; set; }
    }

    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name, MethodInfo method) : base(name)
        {
            Method = method;
        }

        public MethodInfo Method { get; private set; }

        public int ParameterCount
        {
            get
            {
                return Method.GetParameters().Length;
            }
        }
    }
}

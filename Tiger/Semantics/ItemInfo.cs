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
    }

    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name) : base(name) { }
    }
}

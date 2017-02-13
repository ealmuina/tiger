using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.CodeGeneration
{
    class SymbolTable : ICloneable
    {
        public SymbolTable()
        {
            Functions = new Dictionary<string, MethodInfo>();
            Variables = new Dictionary<string, LocalBuilder>();
            Types = DefaultTypes();
        }

        private Dictionary<string, Type> DefaultTypes()
        {
            var types = new Dictionary<string, Type>();

            types["Int"] = typeof(int);
            types["String"] = typeof(string);

            return types;
        }

        public Dictionary<string, MethodInfo> Functions { get; protected set; }

        public Dictionary<string, LocalBuilder> Variables { get; protected set; }

        public Dictionary<string, Type> Types { get; protected set; }

        public Label LoopEnd { get; set; }

        public Type GetType(string type)
        {
            return Types[type];
        }

        public object Clone()
        {
            var clone = new SymbolTable();
            clone.Functions = new Dictionary<string, MethodInfo>(Functions);
            clone.Variables = new Dictionary<string, LocalBuilder>(Variables);
            clone.Types = new Dictionary<string, Type>(Types);
            clone.LoopEnd = LoopEnd;
            return clone;
        }
    }
}

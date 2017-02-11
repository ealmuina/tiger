using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Tiger.CodeGeneration;

namespace Tiger.Semantics
{
    class Scope
    {
        Dictionary<string, ItemInfo> symbols;
        FunctionInfo[] stdl;

        public Scope()
        {
            symbols = new Dictionary<string, ItemInfo>();
            UsedStdlFunctions = new HashSet<string>();
            SetStdl();
        }

        void SetStdl()
        {
            stdl = new[]
            {
                new FunctionInfo("printi", "None", "Int"),
                new FunctionInfo("print", "None", "String"),
                new FunctionInfo("getline", "String", "None"),
                new FunctionInfo("printline", "None", "String"),
                new FunctionInfo("printiline", "None", "Int"),
                new FunctionInfo("ord", "Int", "String"),
                new FunctionInfo("chr", "String", "Int"),
                new FunctionInfo("size", "Int", "String"),
                new FunctionInfo("substring", "String", "String", "Int", "Int"),
                new FunctionInfo("concat", "String", "String", "String"),
                new FunctionInfo("not", "Int", "Int"),
                new FunctionInfo("exit", "None", "Int"),
            };

            foreach (var func in stdl)
                symbols[func.Name] = func;
        }

        public bool IsDefined(string name)
        {
            if (stdl.Where(m => m.Name == name).Count() > 0)
                UsedStdlFunctions.Add(name);
            return symbols.ContainsKey(name);
        }

        public bool IsDefined<TInfo>(string name)
        {
            ItemInfo item = null;
            if (symbols.TryGetValue(name, out item))
                return item is TInfo;
            return false;
        }

        public ItemInfo this[string name]
        {
            get
            {
                return GetItem<ItemInfo>(name);
            }
        }

        public HashSet<string> UsedStdlFunctions { get; protected set; }

        public TInfo GetItem<TInfo>(string name) where TInfo : ItemInfo
        {
            ItemInfo item = null;
            if (symbols.TryGetValue(name, out item) && item is TInfo)
                return (TInfo)item;
            throw new Exception(string.Format("Symbol {0} is not defined", name));
        }

        public VariableInfo DefineVariable(string name, string type)
        {
            if (!IsDefined(name))
            {
                var result = new VariableInfo(name, type);
                symbols.Add(name, result);
                return result;
            }
            else
                throw new Exception(string.Format("Symbol {0} is already defined", name));
        }

        public FunctionInfo DefineFunction(string name, string type)
        {
            if (!IsDefined(name))
            {
                var result = new FunctionInfo(name, type);
                symbols.Add(name, result);
                return result;
            }
            else
                throw new Exception(string.Format("Symbol {0} is already defined", name));
        }
    }
}

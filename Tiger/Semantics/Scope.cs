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
        Dictionary<string, ItemInfo> symbols = new Dictionary<string, ItemInfo>();

        public Scope()
        {
            SetFunctions();
        }

        void SetFunctions()
        {
            //TODO Add standard library functions
        }

        public bool IsDefined(string name)
        {
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

        public TInfo GetItem<TInfo>(string name) where TInfo : ItemInfo
        {
            ItemInfo item = null;
            if (symbols.TryGetValue(name, out item) && item is TInfo)
                return (TInfo)item;
            throw new Exception(string.Format("Symbol {0} is not defined", name));
        }

        public VariableInfo DefineVariable(string name)
        {
            if (!IsDefined(name))
            {
                var result = new VariableInfo(name);
                symbols.Add(name, result);
                return result;
            }
            else
                throw new Exception(string.Format("Symbol {0} is already defined", name));
        }

        public FunctionInfo DefineFunction(string name)
        {
            if (!IsDefined(name))
            {
                var result = new FunctionInfo(name);
                symbols.Add(name, result);
                return result;
            }
            else
                throw new Exception(string.Format("Symbol {0} is already defined", name));
        }
    }
}

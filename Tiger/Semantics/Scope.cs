using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            //TODO Ahora mismo print y printi pinchan pa cualquiera. Arreglar
            symbols["print"] = new FunctionInfo("print", typeof(Console).GetMethod("Write"));
            symbols["printi"] = new FunctionInfo("printi", typeof(Console).GetMethod("Write"));

            symbols["getline"] = new FunctionInfo("getline", typeof(Console).GetMethod("ReadLine"));
            symbols["printline"] = new FunctionInfo("printline", typeof(Console).GetMethod("WriteLine"));
            symbols["printiline"] = new FunctionInfo("printiline", typeof(Console).GetMethod("WriteLine"));

            //TODO symbols["ord"]
            //TODO symbols["chr"]

            symbols["size"] = new FunctionInfo(
                "size", 
                new Func<string, int>(s => s.Length).Method);

            symbols["substring"] = new FunctionInfo(
                "substring",
                new Func<string, int, int, string>((s, f, n) => s.Substring(f, n)).Method);

            symbols["concat"] = new FunctionInfo(
                "concat",
                new Func<string, string, string>((s1, s2) => s1 + s2).Method);

            symbols["not"] = new FunctionInfo(
                "not",
                new Func<int, int>(i => i == 0 ? 1 : 0).Method);

            symbols["exit"] = new FunctionInfo(
                "exit",
                new Action<int>(i => Environment.Exit(i)).Method); 
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
    }
}

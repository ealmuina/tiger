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

        public Scope()
        {
            symbols = new Dictionary<string, ItemInfo>();
            Types = new Dictionary<string, TypeInfo>();

            UsedStdlFunctions = new HashSet<string>();

            SetStdl();
            SetTypes();
        }

        public Scope(Scope other)
        {
            symbols = new Dictionary<string, ItemInfo>(other.symbols);
            Types = new Dictionary<string, TypeInfo>(other.Types);
            InsideLoop = other.InsideLoop;
            UsedStdlFunctions = other.UsedStdlFunctions;
            Stdl = other.Stdl;
        }

        void SetStdl()
        {
            Stdl = new[]
            {
                new FunctionInfo("printi", true, Semantics.Types.Void, Semantics.Types.Int),
                new FunctionInfo("print", true, Semantics.Types.Void, Semantics.Types.String),
                new FunctionInfo("getline", true, Semantics.Types.String),
                new FunctionInfo("printline", true, Semantics.Types.Void, Semantics.Types.String),
                new FunctionInfo("printiline", true, Semantics.Types.Void, Semantics.Types.Int),
                new FunctionInfo("ord", true, Semantics.Types.Int, Semantics.Types.String),
                new FunctionInfo("chr", true, Semantics.Types.String, Semantics.Types.Int),
                new FunctionInfo("size", true, Semantics.Types.Int, Semantics.Types.String),
                new FunctionInfo("substring", true, Semantics.Types.String, Semantics.Types.String, Semantics.Types.Int, Semantics.Types.Int),
                new FunctionInfo("concat", true, Semantics.Types.String, Semantics.Types.String, Semantics.Types.String),
                new FunctionInfo("not", true, Semantics.Types.Int, Semantics.Types.Int),
                new FunctionInfo("exit", true, Semantics.Types.Void, Semantics.Types.Int),
            };

            foreach (var func in Stdl)
                symbols[func.Name] = func;
        }

        void SetTypes()
        {
            Types[Semantics.Types.Int] = new TypeInfo(Semantics.Types.Int, new string[] { }, new string[] { });
            Types[Semantics.Types.String] = new TypeInfo(Semantics.Types.String, new string[] { }, new string[] { });
            Types[Semantics.Types.Void] = new TypeInfo(Semantics.Types.Void, new string[] { }, new string[] { });
            Types[Semantics.Types.Nil] = new TypeInfo(Semantics.Types.Nil, new string[] { }, new string[] { });
        }

        public FunctionInfo[] Stdl { get; protected set; }

        public VariableInfo[] Variables
        {
            get { return symbols.Values.Where(v => v is VariableInfo).Cast<VariableInfo>().ToArray(); }
        }

        public bool IsDefined(string name)
        {
            if (Stdl.Where(m => m.Name == name).Count() > 0)
                UsedStdlFunctions.Add(name);
            return symbols.ContainsKey(name);
        }

        public bool IsDefined<TInfo>(string name)
        {
            if (typeof(TInfo) == typeof(TypeInfo))
            {
                return Types.ContainsKey(name);
            }
            else
            {
                ItemInfo item = null;
                if (symbols.TryGetValue(name, out item))
                    return item is TInfo;
                return false;
            }
        }

        public bool InsideLoop { get; set; }

        public HashSet<string> UsedStdlFunctions { get; protected set; }

        public Dictionary<string, TypeInfo> Types { get; protected set; }

        public ItemInfo this[string name]
        {
            get
            {
                return GetItem<ItemInfo>(name);
            }
        }

        public TInfo GetItem<TInfo>(string name) where TInfo : ItemInfo
        {
            if (typeof(TInfo) == typeof(TypeInfo))
            {
                TypeInfo item = null;
                if (Types.TryGetValue(name, out item))
                {
                    while (item is TypeAlias)
                        item = Types[(item as TypeAlias).Aliased];
                    return item as TInfo;
                }
            }
            else
            {
                ItemInfo item = null;
                if (symbols.TryGetValue(name, out item) && item is TInfo)
                    return (TInfo)item;
            }
            throw new Exception(string.Format("Symbol {0} is not defined", name));
        }

        public VariableInfo DefineVariable(string name, string type, bool readOnly, bool isForeign)
        {
            var result = new VariableInfo(name, type, readOnly, isForeign);
            symbols[name] = result;
            return result;
        }

        public FunctionInfo DefineFunction(string name, string type, params string[] parameters)
        {
            var result = new FunctionInfo(name, false, type, parameters);
            symbols[name] = result;
            return result;
        }

        public TypeInfo DefineType(string name, string[] fieldNames, string[] fieldTypes, bool isArray=false)
        {
            var result = new TypeInfo(name, fieldNames, fieldTypes, isArray);
            Types[name] = result;
            return result;
        }

        public TypeInfo DefineType(string name, string aliased)
        {
            var result = new TypeAlias(name, aliased);
            Types[name] = result;
            return result;
        }

        public bool SameType(string t1, string t2)
        {
            var info1 = GetItem<TypeInfo>(t1);
            var info2 = GetItem<TypeInfo>(t2);
            return info1 == info2;
        }

        public bool BadAlias(string name)
        {
            TypeInfo item = Types[name];
            var visited = new HashSet<TypeInfo>();
            while (item is TypeAlias)
            {
                string next = (item as TypeAlias).Aliased;

                if (!Types.ContainsKey(next) || visited.Contains(item))
                    return true;

                visited.Add(item);
                item = Types[next];
            }
            return false;
        }
    }
}

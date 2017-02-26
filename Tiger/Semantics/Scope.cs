using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Tiger.CodeGeneration;

namespace Tiger.Semantics
{
    class Scope : ICloneable
    {
        Dictionary<string, ItemInfo> symbols;

        public Scope()
        {
            symbols = new Dictionary<string, ItemInfo>();
            DefinedTypes = new Dictionary<string, TypeInfo>();

            UsedStdlFunctions = new HashSet<string>();

            SetStdl();
            SetTypes();
        }

        void SetStdl()
        {
            Stdl = new[]
            {
                new FunctionInfo("printi", true, Types.Void, Types.Int),
                new FunctionInfo("print", true, Types.Void, Types.String),
                new FunctionInfo("getline", true, Types.String),
                new FunctionInfo("printline", true, Types.Void, Types.String),
                new FunctionInfo("printiline", true, Types.Void, Types.Int),
                new FunctionInfo("ord", true, Types.Int, Types.String),
                new FunctionInfo("chr", true, Types.String, Types.Int),
                new FunctionInfo("size", true, Types.Int, Types.String),
                new FunctionInfo("substring", true, Types.String, Types.String, Types.Int, Types.Int),
                new FunctionInfo("concat", true, Types.String, Types.String, Types.String),
                new FunctionInfo("not", true, Types.Int, Types.Int),
                new FunctionInfo("exit", true, Types.Void, Types.Int),
            };

            foreach (var func in Stdl)
                symbols[func.Name] = func;
        }

        void SetTypes()
        {
            DefinedTypes[Types.Int] = new TypeInfo(Types.Int, new string[] { }, new string[] { });
            DefinedTypes[Types.String] = new TypeInfo(Types.String, new string[] { }, new string[] { });
            DefinedTypes[Types.Void] = new TypeInfo(Types.Void, new string[] { }, new string[] { });
            DefinedTypes[Types.Nil] = new TypeInfo(Types.Nil, new string[] { }, new string[] { });
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
                return DefinedTypes.ContainsKey(name);
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

        public Dictionary<string, TypeInfo> DefinedTypes { get; protected set; }

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
                if (DefinedTypes.TryGetValue(name, out item))
                {
                    while (item is TypeAlias)
                        item = DefinedTypes[(item as TypeAlias).Aliased];
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

        public TypeInfo DefineType(string name, string[] fieldNames, string[] fieldTypes)
        {
            var result = new TypeInfo(name, fieldNames, fieldTypes);
            DefinedTypes[name] = result;
            return result;
        }

        public TypeInfo DefineType(string name, string aliased)
        {
            var result = new TypeAlias(name, aliased);
            DefinedTypes[name] = result;
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
            TypeInfo item = DefinedTypes[name];
            var visited = new HashSet<TypeInfo>();
            while (item is TypeAlias)
            {
                string next = (item as TypeAlias).Aliased;

                if (!DefinedTypes.ContainsKey(next) || visited.Contains(item))
                    return true;

                visited.Add(item);
                item = DefinedTypes[next];
            }
            return false;
        }

        public object Clone()
        {
            var clone = new Scope();
            clone.symbols = new Dictionary<string, ItemInfo>(symbols);
            clone.DefinedTypes = new Dictionary<string, TypeInfo>(DefinedTypes);
            clone.InsideLoop = InsideLoop;
            clone.UsedStdlFunctions = UsedStdlFunctions;
            return clone;
        }
    }
}

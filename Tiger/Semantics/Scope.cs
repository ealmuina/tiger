using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiger.Semantics
{
    class Scope
    {
        Dictionary<string, ItemInfo> symbols; // Info relative to variables and functions
        Dictionary<string, TypeInfo> types;   // Info relative to types

        public Scope()
        {
            symbols = new Dictionary<string, ItemInfo>();
            types = new Dictionary<string, TypeInfo>();

            UsedStdlFunctions = new HashSet<string>();

            InitStdl();
            InitTypes();
        }

        public Scope(Scope other)
        {
            // Copy constructor
            symbols = new Dictionary<string, ItemInfo>(other.symbols);
            types = new Dictionary<string, TypeInfo>(other.types);
            InsideLoop = other.InsideLoop;
            UsedStdlFunctions = other.UsedStdlFunctions;
            Stdl = other.Stdl;
        }

        /// <summary>
        /// Initialize information relative to Standard Library.
        /// </summary>
        void InitStdl()
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

        /// <summary>
        /// Initialize information relative to language default types.
        /// </summary>
        void InitTypes()
        {
            types[Types.Int.Name] = Types.Int;
            types[Types.String.Name] = Types.String;
            types[Types.Void.Name] = Types.Void;
            types[Types.Nil.Name] = Types.Nil;
        }

        /// <summary>
        /// Info relative to Standard Library functions
        /// </summary>
        public FunctionInfo[] Stdl { get; protected set; }

        /// <summary>
        /// Info relative to variables in the current scope
        /// </summary>
        public VariableInfo[] Variables
        {
            get { return symbols.Values.Where(v => v is VariableInfo).Cast<VariableInfo>().ToArray(); }
        }

        /// <summary>
        /// Determines if a name is defined in the scope
        /// </summary>
        /// <typeparam name="TInfo">Expected type of the name (variable, function, type)</typeparam>
        /// <param name="name">Name to query for</param>
        /// <returns>True if found, false otherwise</returns>
        public bool IsDefined<TInfo>(string name)
        {
            if (typeof(TInfo) == typeof(TypeInfo))
            {
                return types.ContainsKey(name);
            }
            else
            {
                if (Stdl.Any(m => m.Name == name))
                    UsedStdlFunctions.Add(name);

                if (symbols.TryGetValue(name, out ItemInfo item))
                    return item is TInfo;
                return false;
            }
        }

        /// <summary>
        /// Indicates if current scope is inside a loop, which means that a 'break' is valid on it
        /// </summary>
        public bool InsideLoop { get; set; }

        /// <summary>
        /// Set with the names of invoked stdl functions
        /// </summary>
        public HashSet<string> UsedStdlFunctions { get; protected set; }

        public TInfo GetItem<TInfo>(string name) where TInfo : ItemInfo
        {
            if (typeof(TInfo) == typeof(TypeInfo))
            {
                if (types.TryGetValue(name, out TypeInfo item))
                {
                    while (item is AliasInfo)
                        item = types[(item as AliasInfo).Aliased];
                    return item as TInfo;
                }
            }
            else
            {
                if (symbols.TryGetValue(name, out ItemInfo item) && item is TInfo)
                    return (TInfo)item;
            }
            throw new Exception(string.Format("Symbol {0} is not defined", name));
        }

        #region Define...
        public VariableInfo DefineVariable(string name, string type, bool readOnly, bool isForeign)
        {
            var tInfo = GetItem<TypeInfo>(type);
            return DefineVariable(name, tInfo, readOnly, isForeign);
        }

        public VariableInfo DefineVariable(string name, TypeInfo type, bool readOnly, bool isForeign)
        {
            var result = new VariableInfo(name, type, readOnly, isForeign);
            symbols[name] = result;
            return result;
        }

        public FunctionInfo DefineFunction(string name, string type, params string[] parameters)
        {
            var tInfo = GetItem<TypeInfo>(type);
            TypeInfo[] tParams = parameters.Select(p => GetItem<TypeInfo>(p)).ToArray();

            var result = new FunctionInfo(name, false, tInfo, tParams);
            symbols[name] = result;
            return result;
        }

        public RecordInfo DefineRecordType(string name, string[] fieldNames, string[] fieldTypes)
        {
            var result = new RecordInfo(name, fieldNames, fieldTypes);
            types[name] = result;
            return result;
        }

        public AliasInfo DefineAliasType(string name, string aliased)
        {
            var result = new AliasInfo(name, aliased);
            types[name] = result;
            return result;
        }

        public ArrayInfo DefineArrayType(string name, string elementsType)
        {
            var result = new ArrayInfo(name, elementsType);
            types[name] = result;
            return result;
        }
        #endregion

        /// <summary>
        /// Check if a type name is part of an invalid alias cycle
        /// </summary>
        /// <param name="name">Name of the type</param>
        /// <returns>True if there is a bad alias, false otherwise</returns>
        public bool BadAlias(string name)
        {
            TypeInfo item = types[name];
            var visited = new HashSet<TypeInfo>();
            while (item is AliasInfo || item is ArrayInfo)
            {
                string next = (item is AliasInfo) ?
                    (item as AliasInfo).Aliased :
                    (item as ArrayInfo).ElementsTypeName;

                if (!types.ContainsKey(next) || visited.Contains(item))
                    return true;

                visited.Add(item);
                item = types[next];
            }
            return false;
        }
    }
}

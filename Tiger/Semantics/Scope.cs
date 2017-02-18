﻿using System;
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
        Dictionary<string, TypeInfo> types;
        Dictionary<string, string> parentType;

        public Scope()
        {
            symbols = new Dictionary<string, ItemInfo>();
            types = new Dictionary<string, TypeInfo>();

            parentType = new Dictionary<string, string>();
            parentType[Types.Int]
                = parentType[Types.String]
                = parentType[Types.Nil]
                = parentType[Types.Void]
                = null; //base types

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
            types[Types.Int] = new TypeInfo(Types.Int);
            types[Types.String] = new TypeInfo(Types.String);
        }

        string GetRoot(string t)
        {
            while (parentType[t] != null)
                t = parentType[t];
            return t;
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
                return types.ContainsKey(name);
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

        public VariableInfo DefineVariable(string name, string type, bool readOnly, bool isParam, bool isForeign)
        {
            var result = new VariableInfo(name, type, readOnly, isParam, isForeign);
            symbols[name] = result;
            return result;
        }

        public FunctionInfo DefineFunction(string name, string type, params string[] parameters)
        {
            var result = new FunctionInfo(name, false, type, parameters);
            symbols[name] = result;
            return result;
        }

        public TypeInfo DefineType(string name)
        {
            var result = new TypeInfo(name);
            types[name] = result;
            return result;
        }

        public bool SameType(string t1, string t2)
        {
            string root1 = GetRoot(t1);
            string root2 = GetRoot(t2);
            return root1 == root2;
        }

        public object Clone()
        {
            var clone = new Scope();
            clone.symbols = new Dictionary<string, ItemInfo>(symbols);
            clone.types = new Dictionary<string, TypeInfo>(types);
            clone.parentType = new Dictionary<string, string>(parentType);
            clone.InsideLoop = InsideLoop;
            clone.UsedStdlFunctions = UsedStdlFunctions;
            return clone;
        }
    }
}

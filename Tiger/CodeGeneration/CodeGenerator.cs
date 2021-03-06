﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.IO;
using System.Reflection;
using Tiger.Semantics;

namespace Tiger.CodeGeneration
{
    class CodeGenerator
    {
        /// <summary>
        /// Numeric identifier for types generated names
        /// </summary>
        public static int TypeId { get; set; }

        public CodeGenerator()
        {
            Functions = new Dictionary<string, MethodInfo>();
            Variables = new Dictionary<string, LocalBuilder>();
            Types = DefaultTypes;
            ParamIndex = new Dictionary<string, int>();
            Fields = new Dictionary<RecordInfo, Dictionary<string, FieldBuilder>>();
        }

        public CodeGenerator(CodeGenerator other)
        {
            // Copy constructor

            Module = other.Module;
            Type = other.Type;
            Method = other.Method;
            Assembly = other.Assembly;
            Generator = other.Generator;

            Functions = new Dictionary<string, MethodInfo>(other.Functions);
            Variables = new Dictionary<string, LocalBuilder>(other.Variables);
            Types = new Dictionary<Semantics.TypeInfo, Type>(other.Types);
            ParamIndex = new Dictionary<string, int>(other.ParamIndex);
            Fields = new Dictionary<RecordInfo, Dictionary<string, FieldBuilder>>(other.Fields);
            LoopEnd = other.LoopEnd;
        }

        Dictionary<Semantics.TypeInfo, Type> DefaultTypes
        {
            get
            {
                var types = new Dictionary<Semantics.TypeInfo, Type>();
                types[Semantics.Types.Int] = typeof(int);
                types[Semantics.Types.String] = typeof(string);
                types[Semantics.Types.Void] = typeof(void);
                return types;
            }
        }

        #region Reflection.Emit
        public AssemblyBuilder Assembly { get; set; }

        public ModuleBuilder Module { get; set; }

        public TypeBuilder Type { get; set; }

        public MethodBuilder Method { get; set; }

        public ILGenerator Generator { get; set; }
        #endregion

        /// <summary>
        /// MethodInfo of functions in current scope
        /// </summary>
        public Dictionary<string, MethodInfo> Functions { get; set; }

        /// <summary>
        /// LocalBuilder of variables in current scope
        /// </summary>
        public Dictionary<string, LocalBuilder> Variables { get; set; }

        /// <summary>
        /// System.Type of types in current scope
        /// </summary>
        public Dictionary<Semantics.TypeInfo, Type> Types { get; set; }

        /// <summary>
        /// Dictionary that associates a name of a parameter in a function with its corresponding 0-based position
        /// </summary>
        public Dictionary<string, int> ParamIndex { get; protected set; }

        /// <summary>
        /// Associates the name of a type with a Dictionary that given a field name returns its corresponding FieldBuilder
        /// </summary>
        public Dictionary<RecordInfo, Dictionary<string, FieldBuilder>> Fields { get; protected set; }

        /// <summary>
        /// Label indicating where to jump when breaking a loop
        /// </summary>
        public Label LoopEnd { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.IO;
using System.Reflection;

namespace Tiger.CodeGeneration
{
    class CodeGenerator : ICloneable
    {
        public CodeGenerator(string path)
        {
            FileName = Path.GetFileName(Path.ChangeExtension(path, "exe"));
            Name = Path.GetFileNameWithoutExtension(path);

            Functions = new Dictionary<string, MethodInfo>();
            Variables = new Dictionary<string, LocalBuilder>();
            Types = DefaultTypes();
            ParamIndex = new Dictionary<string, int>();
        }

        protected CodeGenerator(CodeGenerator other)
        {
            Module = other.Module;
            Type = other.Type;
            Method = other.Method;
            Assembly = other.Assembly;
            Generator = other.Generator;

            Name = other.Name;
            FileName = other.FileName;

            Functions = new Dictionary<string, MethodInfo>(other.Functions);
            Variables = new Dictionary<string, LocalBuilder>(other.Variables);
            Types = new Dictionary<string, Type>(other.Types);
            ParamIndex = new Dictionary<string, int>(other.ParamIndex);
            LoopEnd = other.LoopEnd;
        }

        Dictionary<string, Type> DefaultTypes()
        {
            var types = new Dictionary<string, Type>();

            types[Semantics.Types.Int] = typeof(int);
            types[Semantics.Types.String] = typeof(string);
            types[Semantics.Types.Void] = typeof(void);

            return types;
        }

        public ModuleBuilder Module { get; set; }

        public TypeBuilder Type { get; set; }

        public MethodBuilder Method { get; set; }

        public AssemblyBuilder Assembly { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public ILGenerator Generator { get; set; }

        public Dictionary<string, MethodInfo> Functions { get; protected set; }

        public Dictionary<string, LocalBuilder> Variables { get; protected set; }

        public Dictionary<string, Type> Types { get; protected set; }

        public Dictionary<string, int> ParamIndex { get; protected set; }

        public Label LoopEnd { get; set; }

        public Type GetType(string type)
        {
            return Types[type];
        }

        public object Clone()
        {
            return new CodeGenerator(this);
        }
    }
}

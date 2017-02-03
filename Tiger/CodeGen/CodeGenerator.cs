using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.IO;

namespace Tiger.CodeGen
{
    class CodeGenerator
    {
        public CodeGenerator(string path)
        {
            FileName = Path.GetFileName(Path.ChangeExtension(path, "exe"));
            Name = Path.GetFileNameWithoutExtension(path);
        }

        public ModuleBuilder Module { get; set; }

        public TypeBuilder Type { get; set; }

        public AssemblyBuilder Assembly { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public ILGenerator Generator { get; set; }

        public int LetCount { get; set; }

        public int VariableCount { get; set; }

        public int FunctionCount { get; set; }

        public int FieldCount { get; set; }
    }
}

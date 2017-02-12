using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.CodeGeneration
{
    class SymbolTable
    {
        public Dictionary<string, MethodInfo> Functions { get; } = new Dictionary<string, MethodInfo>();

        public Label LoopEnd { get; set; }
    }
}

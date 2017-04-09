using System.Collections.Generic;

namespace Tiger.Semantics
{
    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name, bool inStdl, TypeInfo returnType, params TypeInfo[] parameters)
            : base(name, returnType)
        {
            IsStdlFunc = inStdl;
            Parameters = parameters;
        }

        public TypeInfo[] Parameters { get; }

        public bool IsStdlFunc { get; }

        public List<string> ForeignVars { get; } = new List<string>(); //stores the names of the foreign variables visible to the function
    }
}

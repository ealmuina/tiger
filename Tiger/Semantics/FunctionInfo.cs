using System.Collections.Generic;

namespace Tiger.Semantics
{
    class FunctionInfo : ItemInfo
    {
        public FunctionInfo(string name, bool inStdl, string returnType, params string[] parameters)
            : base(name, returnType)
        {
            IsStdlFunc = inStdl;
            Parameters = parameters;
        }

        public string[] Parameters { get; protected set; }

        public bool IsStdlFunc { get; protected set; }

        public List<string> ForeignVars { get; } = new List<string>(); //stores the names of the foreign variables visible to the function
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Semantics
{
    class VariableInfo : ItemInfo
    {
        public VariableInfo(string name, string type, bool readOnly, bool isForeign) : base(name, type)
        {
            IsReadOnly = readOnly;
            IsForeign = isForeign;
        }

        public bool IsReadOnly { get; protected set; }

        public bool IsForeign { get; protected set; }
    }
}

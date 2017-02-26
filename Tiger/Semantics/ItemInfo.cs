using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger.Semantics
{
    abstract class ItemInfo
    {
        protected ItemInfo(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; protected set; }

        public string Type { get; protected set; }
    }  
}

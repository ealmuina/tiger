#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()

namespace Tiger.Semantics
{
    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }

        static public bool operator ==(TypeInfo a, TypeInfo b)
        {
            return a.Equals(b) ||
                (!a.Equals(Types.Void) && !b.Equals(Types.Void) &&
                (a.Equals(Types.Nil) || b.Equals(Types.Nil)));
        }

        static public bool operator !=(TypeInfo a, TypeInfo b)
        {
            return !(a == b);
        }

        public override string ToString() => Name;
    }

    class RecordInfo : TypeInfo
    {
        public RecordInfo(string name, string[] fieldNames, string[] fieldTypes) : base(name)
        {
            FieldNames = fieldNames;
            FieldTypesNames = fieldTypes;
        }

        public string[] FieldNames { get; }

        public string[] FieldTypesNames { get; }

        public TypeInfo[] FieldTypes { get; set; }
    }

    class AliasInfo : TypeInfo
    {
        public AliasInfo(string name, string aliased) : base(name)
        {
            Aliased = aliased;
        }

        public string Aliased { get; }
    }

    class ArrayInfo : TypeInfo
    {
        public ArrayInfo(string name, string elementsType) : base(name)
        {
            ElementsTypeName = elementsType;
        }

        public string ElementsTypeName { get; }

        public TypeInfo ElementsType { get; set; }
    }
}

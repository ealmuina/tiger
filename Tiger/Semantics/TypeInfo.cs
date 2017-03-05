namespace Tiger.Semantics
{
    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }              
    }

    class RecordInfo : TypeInfo
    {
        public RecordInfo(string name, string[] fieldNames, string[] fieldTypes) : base(name)
        {
            FieldNames = fieldNames;
            FieldTypes = fieldTypes;
        }

        public string[] FieldNames { get; protected set; }

        public string[] FieldTypes { get; protected set; }
    }

    class AliasInfo : TypeInfo
    {
        public AliasInfo(string name, string aliased) : base(name)
        {
            Aliased = aliased;
        }

        public string Aliased { get; protected set; }
    }

    class ArrayInfo : TypeInfo
    {
        public ArrayInfo(string name, string elementsType) : base(name)
        {
            ElementsType = elementsType;
        }

        public string ElementsType { get; protected set; }
    }
}

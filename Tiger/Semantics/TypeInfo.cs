namespace Tiger.Semantics
{
    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }

        public TypeInfo(string name, string[] fieldNames, string[] fieldTypes) : base(name, Types.Void)
        {
            FieldNames = fieldNames;
            FieldTypes = fieldTypes;
        }

        public string[] FieldNames { get; protected set; }

        public string[] FieldTypes { get; protected set; }
    }

    class TypeAlias : TypeInfo
    {
        public TypeAlias(string name, string aliased, bool isArray=false) : base(name)
        {
            Aliased = aliased;
            IsArray = isArray;
        }

        public string Aliased { get; protected set; }

        public bool IsArray { get; protected set; }
    }
}

namespace Tiger.Semantics
{
    class TypeInfo : ItemInfo
    {
        public TypeInfo(string name) : base(name, Types.Void) { }

        public TypeInfo(string name, string[] fieldNames, string[] fieldTypes, bool isArray=false) : base(name, Types.Void)
        {
            FieldNames = fieldNames;
            FieldTypes = fieldTypes;
            IsArray = isArray;
        }

        public bool IsArray { get; protected set; }

        public string[] FieldNames { get; protected set; }

        public string[] FieldTypes { get; protected set; }
    }

    class TypeAlias : TypeInfo
    {
        public TypeAlias(string name, string aliased) : base(name)
        {
            Aliased = aliased;
        }

        public string Aliased { get; protected set; }
    }
}

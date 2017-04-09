namespace Tiger.Semantics
{
    class VariableInfo : ItemInfo
    {
        public VariableInfo(string name, TypeInfo type, bool readOnly, bool isForeign) : base(name, type)
        {
            IsReadOnly = readOnly;
            IsForeign = isForeign;
        }

        public bool IsReadOnly { get; }

        public bool IsForeign { get; }
    }
}

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

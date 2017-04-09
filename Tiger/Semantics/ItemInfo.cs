namespace Tiger.Semantics
{
    abstract class ItemInfo
    {
        protected ItemInfo(string name, TypeInfo type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public TypeInfo Type { get; }
    }  
}

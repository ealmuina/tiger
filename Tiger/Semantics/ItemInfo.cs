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

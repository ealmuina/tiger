namespace Tiger.Semantics
{
    static class Types
    {
        public static TypeInfo Int { get; } = new TypeInfo("int");

        public static TypeInfo String { get; } = new TypeInfo("string");

        public static TypeInfo Nil { get; } = new TypeInfo("nil");

        public static TypeInfo Void { get; } = new TypeInfo("void");
    }
}

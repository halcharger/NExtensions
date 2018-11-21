using NExtensions;

namespace Tests
{
    public class TestClass
    {
        public TestClass()
        {
        }

        public TestClass(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return new[] {Id.ToNullSafeString(), Name, Email}.JoinWithComma(StringJoinOptions.AddSpaceSuffix);
        }
    }
}
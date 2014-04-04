using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void ToNullSafeString_ShouldReturnEmptyStringByDefaultWithNullObject()
        {
            TestClass input = null;

            input.ToNullSafeString().Should().Be(string.Empty);
        }

        [Test]
        public void ToNullSafeString_ShouldReturnSpecifiedStringWhenInputNull()
        {
            TestClass input = null;

            input.ToNullSafeString("NULL").Should().Be("NULL");
        }

        [Test]
        public void ToNullSafeString_ShouldReturnInputToStringWhenInputNotNull()
        {
            var input = new TestClass(1, "name1"){Email = "name1@gmail.com"};

            input.ToNullSafeString().Should().Be(input.ToString());
        }
    }
}
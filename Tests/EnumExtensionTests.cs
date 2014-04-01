using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EnumExtensionTests
    {
        [Test]
        public void Should_Return_ValueTwoDescription()
        {
            TestEnumn.ValueTwo.GetDescription().Should().Be("Value Two");
        }

        [Test]
        public void Should_Return_ValueOneToString()
        {
            TestEnumn.ValueOne.GetDescription().Should().Be("ValueOne");
        }
    }

    public enum TestEnumn
    {
        ValueOne, 
        [System.ComponentModel.Description("Value Two")]
        ValueTwo, 
        ValueThree
    }
}
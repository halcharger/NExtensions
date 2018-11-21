using System;
using System.Linq;
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
            TestEnum.ValueTwo.GetDescription().Should().Be("Value Two");
        }

        [Test]
        public void Should_Return_ValueOneToString()
        {
            TestEnum.ValueOne.GetDescription().Should().Be("ValueOne");
        }

        [Test]
        public void Should_GetListOfEnumValues()
        {
            var values = Enums.GetValues<TestEnum>().ToList();

            values[0].Should().Be(TestEnum.ValueOne);
            values[1].Should().Be(TestEnum.ValueTwo);
            values[2].Should().Be(TestEnum.ValueThree);
        }

        [Test]
        public void Should_GetEnumFromStringValueOf_ValueTwo()
        {
            var enumValue = "ValueTwo".ToEnum<TestEnum>();

            enumValue.Should().Be(TestEnum.ValueTwo);
        }

        [Test]
        public void Should_GetEnumFromDescriptionValueOf_ValueTwo()
        {
            var enumValue = "Value Two".ToEnum<TestEnum>();

            enumValue.Should().Be(TestEnum.ValueTwo);
        }

        [Test]
        public void Should_ThrowArgumentException_WhenGettingEnumValueOfNull()
        {
            string nullString = null;
            
            Assert.Throws<ArgumentException>(() => nullString.ToEnum<TestEnum>(), "Cannot find enum value '<NULL>' for type TestEnum");
        }

        [Test]
        public void Should_ThrowArgumentException_WhenGettingEnumValueOfBoom()
        {
            Assert.Throws<ArgumentException>(() =>"boom".ToEnum<TestEnum>(), "Cannot find enum value 'boom' for type TestEnum");
        }
    }

    public enum TestEnum
    {
        ValueOne,

        [System.ComponentModel.Description("Value Two")]
        ValueTwo,
        ValueThree
    }

    public struct TestStruct
    {
    }
}
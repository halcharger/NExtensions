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

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "Value must be of Enum type.")]
        public void Should_ThrowException_WhenTryingToGetDescriptionOfNonEnumType()
        {
            new TestStruct().GetDescription();
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

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "Cannot find enum value '<NULL>' for type TestEnum")]
        public void Should_ThrowArgumentException_WhenGettingEnumValueOfNull()
        {
            string nullString = null;
            nullString.ToEnum<TestEnum>();
        }

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "Cannot find enum value 'boom' for type TestEnum")]
        public void Should_ThrowArgumentException_WhenGettingEnumValueOfBoom()
        {
            "boom".ToEnum<TestEnum>();
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
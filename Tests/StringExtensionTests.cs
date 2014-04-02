using System;
using System.Linq;
using FluentAssertions;
using NExtensions;
using NUnit.Framework;
using StringSplitOptions = NExtensions.StringSplitOptions;

namespace Tests
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void IsNullOrEmpty_ShouldReturn_True_WhenStringIsNull()
        {
            string value = null;

            value.IsNullOrEmpty().Should().BeTrue();
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturn_True_WhenStringIsEmpty()
        {
            string value = "";

            value.IsNullOrEmpty().Should().BeTrue();
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturn_False_WhenStringHasValue()
        {
            string value = "boom";

            value.IsNullOrEmpty().Should().BeFalse();
        }

        [Test]
        public void IsNullOrWhiteSpace_ShouldReturn_True_WhenStringIsNull()
        {
            string value = null;

            value.IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Test]
        public void IsNullOrWhiteSpace_ShouldReturn_True_WhenStringIsEmpty()
        {
            string value = "";

            value.IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Test]
        public void IsNullOrWhiteSpace_ShouldReturn_True_WhenStringWhiteSpace()
        {
            string value = "   ";

            value.IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Test]
        public void IsNullOrWhiteSpace_ShouldReturn_False_WhenStringHasValue()
        {
            string value = "boom";

            value.IsNullOrWhiteSpace().Should().BeFalse();
        }

        [Test]
        public void FormatWith_CorrectlyFormatsString()
        {
            "1 {0} 2 {1}".FormatWith("one", "two").Should().Be("1 one 2 two");
        }

        [Test]
        public void Append_AddsTwoStringsTogether()
        {
            "one".Append("two").Should().Be("onetwo");
        }

        [Test]
        public void JoinWithComma_JoinsStringsWithComma()
        {
            var strings = new[] {"one", "two", "three"};

            strings.JoinWithComma().Should().Be("one,two,three");
        }

        [Test]
        public void JoinWithCommaAndaddedSpace_JoinsStringsWithComma()
        {
            var strings = new[] { "one", "two", "three" };

            strings.JoinWithComma(StringJoinOptions.AddSpaceSuffix).Should().Be("one, two, three");
        }

        [Test]
        public void JoinWithSemiColon_JoinsStringsWithSemiColon()
        {
            var strings = new[] { "one", "two", "three" };

            strings.JoinWithSemiColon().Should().Be("one;two;three");
        }

        [Test]
        public void JoinWithSemiColonAndAddedSpace_JoinsStringsWithSemiColon()
        {
            var strings = new[] { "one", "two", "three" };

            strings.JoinWithSemiColon(StringJoinOptions.AddSpaceSuffix).Should().Be("one; two; three");
        }

        [Test]
        public void JoinWithNewLine_JoinsStringsWithSemiNewLine()
        {
            var strings = new[] { "one", "two", "three" };

            strings.JoinWithNewLine().Should().Be("one\r\ntwo\r\nthree");
        }

        [Test]
        public void JoinWithDash_JoinsStringsWithDash()
        {
            var strings = new[] { "one", "two", "three" };

            strings.JoinWith("-").Should().Be("one-two-three");
        }

        [Test]
        public void Remove_CorrectlyRemovesText()
        {
            "onetwothree".Remove("two").Should().Be("onethree");
        }

        [Test]
        public void RemoveDoesNothingWhenStringToRemoveNotFound()
        {
            "onetwothree".Remove("blah").Should().Be("onetwothree");
        }

        [Test]
        public void SplitBy_ReturnsEnumerableSplitBySeperatorWithRemoveEmptyEntriesSetByDefault()
        {
            var value = "one/two /three/ /";
            var values = value.SplitBy("/").ToArray();

            values.Length.Should().Be(4);
            values[0].Should().Be("one");
            values[1].Should().Be("two ");
            values[2].Should().Be("three");
            values[3].Should().Be(" ");
        }

        [Test]
        public void SplitBy_ReturnsEnumerableSplitBySeperatorWithOnlyTrimWhiteSpace()
        {
            var value = "one/two /three/ /";
            var values = value.SplitBy("/", StringSplitOptions.TrimWhiteSpaceFromEntries).ToArray();

            values.Length.Should().Be(5);
            values[0].Should().Be("one");
            values[1].Should().Be("two");
            values[2].Should().Be("three");
            values[3].Should().Be("");
            values[4].Should().Be("");
        }

        [Test]
        public void SplitBy_ReturnsEnumerableSplitBySeperatorWithTrimWhiteSpaceAndRemoveEmptyEntries()
        {
            var value = "one/two /three/ /";
            var values = value.SplitBy("/", StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries).ToArray();

            values.Length.Should().Be(3);
            values[0].Should().Be("one");
            values[1].Should().Be("two");
            values[2].Should().Be("three");
        }

        [Test]
        public void SplitByComma_SplitsByComma()
        {
            var value = "one,two ,three, ";
            var values = value.SplitByComma(StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries).ToArray();

            values.Length.Should().Be(3);
            values[0].Should().Be("one");
            values[1].Should().Be("two");
            values[2].Should().Be("three");
        }

        [Test]
        public void SplitBySemiColon_SplitsBySemiColon()
        {
            var value = "one;two ;three; ";
            var values = value.SplitBySemiColon(StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries).ToArray();

            values.Length.Should().Be(3);
            values[0].Should().Be("one");
            values[1].Should().Be("two");
            values[2].Should().Be("three");
        }

        [Test]
        public void SplitByNewLine_SplitsByNewLine()
        {
            var value = string.Concat("one", Environment.NewLine, "two ", Environment.NewLine, "three", Environment.NewLine, " ");
            var values = value.SplitByNewLine(StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries).ToArray();

            values.Length.Should().Be(3);
            values[0].Should().Be("one");
            values[1].Should().Be("two");
            values[2].Should().Be("three");
        }

        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("true", true)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        [TestCase("false", false)]
        [TestCase("On", true)]
        [TestCase("ON", true)]
        [TestCase("on", true)]
        [TestCase("Off", false)]
        [TestCase("OFF", false)]
        [TestCase("off", false)]
        [TestCase("1", true)]
        [TestCase("0", false)]
        public void ToBoolean_ShouldConvert(string valueToConvert, bool expectedValue)
        {
            valueToConvert.ToBoolean().Should().Be(expectedValue);
        }

        [TestCase("123.45", 123.45)]
        [TestCase("12,345.67", 12345.67)]
        [TestCase("(123.45)", 123.45)]
        [TestCase("-", 0)]
        [TestCase("123.45%", 1.2345)]
        [TestCase("1.845E-07", 0.0000001845)]
        [TestCase("1.845e-07", 0.0000001845)]
        public void ToDecimal_ShouldConvert(string valueToConvert, decimal expectedValue)
        {
            valueToConvert.ToDecimal().Should().Be(expectedValue);
        }

        [TestCase("123.45", 123)]
        [TestCase("12,345.67", 12345)]
        [TestCase("(123.45)", 123)]
        [TestCase("-", 0)]
        public void ToInteger_ShouldConvert(string valueToConvert, int expectedValue)
        {
            valueToConvert.ToInteger().Should().Be(expectedValue);
        }

        [TestCase(null)]
        [TestCase("()")]
        [TestCase(",")]
        [TestCase("")]
        [TestCase(" ")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Cannot convert empty value to Integer")]
        public void ToInteger_ShouldThrowException(string valueToConvert)
        {
            valueToConvert.ToInteger();
        }

        [TestCase(null)]
        [TestCase("()")]
        [TestCase(",")]
        [TestCase("")]
        [ExpectedException(typeof(ArgumentException))]
        public void ToDecimal_ShouldThrowException(string valueToConvert)
        {
            valueToConvert.ToDecimal();
        }

    }
}
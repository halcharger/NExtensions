using FluentAssertions;
using NExtensions;
using NUnit.Framework;

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

            strings.JoinWithComma(StringJoinOptions.AddSpace).Should().Be("one, two, three");
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

            strings.JoinWithSemiColon(StringJoinOptions.AddSpace).Should().Be("one; two; three");
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
    }
}
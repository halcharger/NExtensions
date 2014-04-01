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
    }
}
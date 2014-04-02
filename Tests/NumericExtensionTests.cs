using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NumericExtensionTests
    {
        [TestCase(-123.45, 123.45)]
        [TestCase(123.45, 123.45)]
        public void DecimalAbsolute_ShouldReturnAbsoluteValue(decimal input, decimal output)
        {
            input.Absolute().Should().Be(output);
        }

        [TestCase(-123, 123)]
        [TestCase(123, 123)]
        public void IntegerAbsolute_ShouldReturnAbsoluteValue(int input, int output)
        {
            input.Absolute().Should().Be(output);
        }

    }
}
using System;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExceptionExtensionTests
    {
        [Test]
        public void GetBaseException_ShouldReturnInnerMostException()
        {
            var bottomEx = new Exception("bottom exception");
            var middleEx = new Exception("middle exception", bottomEx);
            var topEx = new Exception("top exception", middleEx);

            topEx.GetBaseException().Should().Be(bottomEx);
        }
    }
}
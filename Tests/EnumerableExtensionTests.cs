using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        [Test]
        public void Foreach_ShouldExecuteActionForeachItemInEnumerable()
        {
            var enumberable = Enumerable.Range(1, 5);
            var str = "testing";
            Action<int> action = i => str = str + i.ToString(); 

            enumberable.ForEach(action);

            str.Should().Be("testing12345");
        }

        [Test]
        public void Empty_IsNullSafe()
        {
            IEnumerable<string> strings = null;

            strings.Empty().Should().BeTrue();
        }

        [Test]
        public void Emtpy_ReturnsTrueWhenActuallyIsEmpty()
        {
            string[] strings = {};

            strings.Empty().Should().BeTrue();
        }

        [Test]
        public void Empty_ReturnFalseWhenEnumerableContainsValues()
        {
            string[] strings = {"boom"};

            strings.Empty().Should().BeFalse();
        }
    }
}
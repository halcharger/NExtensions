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
        public void None_IsNullSafe()
        {
            IEnumerable<string> strings = null;

            strings.None().Should().BeTrue();
        }

        [Test]
        public void None_ReturnsTrueWhenActuallyIsEmpty()
        {
            string[] strings = {};

            strings.None().Should().BeTrue();
        }

        [Test]
        public void None_ReturnFalseWhenEnumerableContainsValues()
        {
            string[] strings = {"boom"};

            strings.None().Should().BeFalse();
        }

        [Test]
        public void NoneWithPredicate_ReturnsTrueWhenNoItemsMeetPredicate()
        {
            string[] strings = {"one", "two", "three"};

            strings.None(s => s == "four").Should().BeTrue();
        }

        [Test]
        public void NoneWithPredicate_ReturnsFalseWhenItemsMeetPredicate()
        {
            string[] strings = { "one", "two", "three" };

            strings.None(s => s == "two").Should().BeFalse();
        }

        [TestCase("1,2,3,4,5,", "2,3,4", true)]
        [TestCase("1,2,3,4,5,", "2,3,4,6", false)]
        public void ContainsAll_WorksCorrectly(string listToCheck, string itemsToCheckFor, bool expectedValue)
        {
            listToCheck.SplitByComma().ContainsAll(itemsToCheckFor.SplitByComma()).Should().Be(expectedValue);
        }

        [TestCase("1,2,3,4,5,", "6,7,8", false)]
        [TestCase("1,2,3,4,5,", "2,3,4", true)]
        public void ContainsNone_WorksCorrectly(string listToCheck, string itemsToCheckFor, bool expectedValue)
        {
            listToCheck.SplitByComma().ContainsAll(itemsToCheckFor.SplitByComma()).Should().Be(expectedValue);
        }

    }
}
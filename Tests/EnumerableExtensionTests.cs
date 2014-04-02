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

        [TestCase("one,two,three,one,four,two", "one,two")]
        public void GetDuplicates_ReturnsDuplicates(string list, string expectedDuplicates)
        {
            list.SplitByComma().GetDuplicates().JoinWithComma().Should().Be(expectedDuplicates);
        }

        [Test]
        public void GetDuplicates_ShouldGroupOnSpecifiedField()
        {
            var items = new[]
            {
                new TestClass(1, "name1"), 
                new TestClass(2, "name2"), 
                new TestClass(3, "name3"), 
                new TestClass(4, "name4"), 
                new TestClass(5, "name1"), 
                new TestClass(6, "name2"), 
                new TestClass(7, "name1")
            };

            var duplicates = items.GetDuplicates(x => x.Name);

            duplicates.Count().Should().Be(2);

            var name1Duplicates = duplicates.First().ToList();
            name1Duplicates.Count().Should().Be(3);
            name1Duplicates[0].Id.Should().Be(1);
            name1Duplicates[1].Id.Should().Be(5);
            name1Duplicates[2].Id.Should().Be(7);

            var name2Duplicates = duplicates.Last().ToList();
            name2Duplicates.Count().Should().Be(2);
            name2Duplicates[0].Id.Should().Be(2);
            name2Duplicates[1].Id.Should().Be(6);

        }
    }

    public class TestClass
    {
        public TestClass() { }

        public TestClass(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
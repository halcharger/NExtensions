using System;
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

            enumberable.Foreach(action);

            str.Should().Be("testing12345");
        }
    }
}
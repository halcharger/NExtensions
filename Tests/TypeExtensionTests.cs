using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void IsString_works()
        {
            GetExpectedTypeValues(typeof(string)).ForEach(kvp => kvp.Key.IsString().Should().Be(kvp.Value));
        }

        [Test]
        public void IsIEnumerable_works()
        {
            new List<int>().GetType().IsIEnumerable().Should().BeTrue();
            "string is IEnumerable".GetType().IsIEnumerable().Should().BeTrue();

            typeof (object).IsIEnumerable().Should().BeFalse();
        }

        [Test]
        public void IsIList_works()
        {
            new List<int>().GetType().IsIList().Should().BeTrue();
            typeof(object).IsIEnumerable().Should().BeFalse();
        }

        [Test]
        public void IsNotIEnumerable_works()
        {
            new List<int>().GetType().IsNotIEnumerable().Should().BeFalse();
            "string is IEnumerable".GetType().IsNotIEnumerable().Should().BeFalse();

            typeof(object).IsNotIEnumerable().Should().BeTrue();
        }

        [Test]
        public void IsNotIList_works()
        {
            new List<int>().GetType().IsNotIList().Should().BeFalse();
            typeof(object).IsNotIEnumerable().Should().BeTrue();
        }


        [Test]
        public void IsDecimal_works()
        {
            GetExpectedTypeValues(typeof(decimal)).ForEach(kvp => kvp.Key.IsDecimal().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNullableDecimal_works()
        {
            GetExpectedTypeValues(typeof(decimal?)).ForEach(kvp => kvp.Key.IsNullableDecimal().Should().Be(kvp.Value));
        }

        [Test]
        public void IsInt_works()
        {
            GetExpectedTypeValues(typeof(int)).ForEach(kvp => kvp.Key.IsInt().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNullableInt_works()
        {
            GetExpectedTypeValues(typeof(int?)).ForEach(kvp => kvp.Key.IsNullableInt().Should().Be(kvp.Value));
        }

        [Test]
        public void IsBool_works()
        {
            GetExpectedTypeValues(typeof(bool)).ForEach(kvp => kvp.Key.IsBool().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNullableBool_works()
        {
            GetExpectedTypeValues(typeof(bool?)).ForEach(kvp => kvp.Key.IsNullableBool().Should().Be(kvp.Value));
        }

        [Test]
        public void IsDateTime_works()
        {
            GetExpectedTypeValues(typeof(DateTime)).ForEach(kvp => kvp.Key.IsDateTime().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNullableDateTime_works()
        {
            GetExpectedTypeValues(typeof(DateTime?)).ForEach(kvp => kvp.Key.IsNullableDateTime().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotString_works()
        {
            GetExpectedTypeValues(typeof(string), true).ForEach(kvp => kvp.Key.IsNotString().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotDecimal_works()
        {
            GetExpectedTypeValues(typeof(decimal), true).ForEach(kvp => kvp.Key.IsNotDecimal().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotNullableDecimal_works()
        {
            GetExpectedTypeValues(typeof(decimal?), true).ForEach(kvp => kvp.Key.IsNotNullableDecimal().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotInt_works()
        {
            GetExpectedTypeValues(typeof(int), true).ForEach(kvp => kvp.Key.IsNotInt().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotNullableInt_works()
        {
            GetExpectedTypeValues(typeof(int?), true).ForEach(kvp => kvp.Key.IsNotNullableInt().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotBool_works()
        {
            GetExpectedTypeValues(typeof(bool), true).ForEach(kvp => kvp.Key.IsNotBool().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotNullableBool_works()
        {
            GetExpectedTypeValues(typeof(bool?), true).ForEach(kvp => kvp.Key.IsNotNullableBool().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotDateTime_works()
        {
            GetExpectedTypeValues(typeof(DateTime), true).ForEach(kvp => kvp.Key.IsNotDateTime().Should().Be(kvp.Value));
        }

        [Test]
        public void IsNotNullableDateTime_works()
        {
            GetExpectedTypeValues(typeof(DateTime?), true).ForEach(kvp => kvp.Key.IsNotNullableDateTime().Should().Be(kvp.Value));
        }

        private IEnumerable<KeyValuePair<Type, bool>> GetExpectedTypeValues(Type typeBeingTested, bool baseValue = false)
        {
            var values = new List<KeyValuePair<Type, bool>>
            {
                new KeyValuePair<Type, bool>(typeof(string), baseValue),
                new KeyValuePair<Type, bool>(typeof(decimal), baseValue),
                new KeyValuePair<Type, bool>(typeof(decimal?), baseValue),
                new KeyValuePair<Type, bool>(typeof(int), baseValue),
                new KeyValuePair<Type, bool>(typeof(int?), baseValue),
                new KeyValuePair<Type, bool>(typeof(DateTime), baseValue),
                new KeyValuePair<Type, bool>(typeof(DateTime?), baseValue),
                new KeyValuePair<Type, bool>(typeof(bool), baseValue),
                new KeyValuePair<Type, bool>(typeof(bool?), baseValue),
                new KeyValuePair<Type, bool>(typeof(Type), baseValue),
                new KeyValuePair<Type, bool>(typeof(IEnumerable<>), baseValue),
                new KeyValuePair<Type, bool>(typeof(IList<>), baseValue),
            };

            var toRemove = values.Single(kvp => kvp.Key == typeBeingTested);
            values.Remove(toRemove);
            values.Add(new KeyValuePair<Type, bool>(typeBeingTested, !baseValue));
            return values;
        }
    }
}
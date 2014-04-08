using System;
using System.Collections.Generic;
using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void ToNullSafeString_ShouldReturnEmptyStringByDefaultWithNullObject()
        {
            TestClass input = null;

            input.ToNullSafeString().Should().Be(string.Empty);
        }

        [Test]
        public void ToNullSafeString_ShouldReturnSpecifiedStringWhenInputNull()
        {
            TestClass input = null;

            input.ToNullSafeString("NULL").Should().Be("NULL");
        }

        [Test]
        public void ToNullSafeString_ShouldReturnInputToStringWhenInputNotNull()
        {
            var input = new TestClass(1, "name1"){Email = "name1@gmail.com"};

            input.ToNullSafeString().Should().Be(input.ToString());
        }

        [Test]
        public void Clone_ShouldDeepCopyObject()
        {
            var source = new CloneClass(1, "name1", Dates.MondayDate, true, 1.1M, typeof(string))
            {
                TheIntsArray = new[]{1, 2, 3, 4, 5},
                TheIntsList = new List<int>{2, 3, 4, 5, 6},
                TheStruct = new StructObj(2, "name2", Dates.TuesdayDate, false, 2.2M, typeof(decimal)),
                TheCloneClassList = new List<CloneClass>
                {
                    new CloneClass(5, "name5", Dates.FridayDate, true, 5.5M, typeof(StructObj)), 
                    new CloneClass(6, "name6", Dates.SaturdayDate, false, 5.5M, typeof(DateTime))
                },
                Child = new CloneClass(3, "name3", Dates.WednesdayDate, false, 3.3M, typeof(bool))
                {
                    TheIntsArray = new[] { 5, 4, 3, 2, 1 },
                    TheIntsList = new List<int> { 6, 5, 4, 3, 2 },
                    TheStruct = new StructObj(4, "name4", Dates.ThursdayDate, true, 4.4M, typeof(CloneClass))
                }
            };

            var clone = source.Clone();

            AssertCloneProperties(clone, source);
            AssertStructProperties(clone.TheStruct, source.TheStruct);

            clone.Child.Should().NotBeNull();
            AssertCloneProperties(clone.Child, source.Child);
            AssertStructProperties(clone.Child.TheStruct, source.Child.TheStruct);

            clone.Should().NotBe(source);//ref check
        }

        [Test]
        public void GetValueForProperty_works()
        {
            var obj = new CloneClass(1, "string1", DateTime.Now, true, 1.1M, typeof(CloneClass));

            obj.GetValueForProperty("TheInt").Should().Be(obj.TheInt);
            obj.GetValueForProperty("TheString").Should().Be(obj.TheString);
            obj.GetValueForProperty("TheDateTime").Should().Be(obj.TheDateTime);
            obj.GetValueForProperty("TheBool").Should().Be(obj.TheBool);
            obj.GetValueForProperty("TheDecimal").Should().Be(obj.TheDecimal);
            obj.GetValueForProperty("TheType").Should().Be(obj.TheType);
        }

        [Test]
        public void GetValueForProperty_returns_defaultValue_when_property_doesnt_exist()
        {
            var obj = new CloneClass();

            obj.GetValueForProperty("NonExistantProperty").Should().BeNull();
            obj.GetValueForProperty("NonExistantProperty", "doesnt exist").Should().Be("doesnt exist");
        }

        [Test]
        public void GetValueForPropert_IsNullSafe()
        {
            CloneClass obj = null;

            obj.GetValueForProperty("TheInt").Should().BeNull();
        }

        [Test]
        public void SetValueForProperty_IsNullSafe()
        {
            CloneClass obj = null;

            obj.SetValueForProperty("TheInt", 1);

            obj.Should().BeNull();
        }

        [Test]
        public void SetValueForProperty_Returns_WhenTryingToSetPropertyThatDoesntExist()
        {
            var obj = new CloneClass();

            obj.SetValueForProperty("NonExistantProperty", "boom");
        }

        [Test]
        public void SetValueForProperty_works()
        {
            var obj = new CloneClass();
            var date = DateTime.Now;

            obj.SetValueForProperty("TheInt", 1);
            obj.SetValueForProperty("TheString", "some string");
            obj.SetValueForProperty("TheDateTime", date);
            obj.SetValueForProperty("TheBool", true);
            obj.SetValueForProperty("TheDecimal", 1.1M);
            obj.SetValueForProperty("TheType", typeof(CloneClass));

            obj.TheInt.Should().Be(1);
            obj.TheString.Should().Be("some string");
            obj.TheDateTime.Should().Be(date);
            obj.TheBool.Should().Be(true);
            obj.TheDecimal.Should().Be(1.1M);
            obj.TheType.Should().Be(typeof(CloneClass));

        }

        private void AssertCloneProperties(CloneClass clone, CloneClass source)
        {
            ReferenceEquals(clone, source).Should().BeFalse();

            clone.TheInt.Should().Be(source.TheInt);

            clone.TheString.Should().Be(source.TheString);
            ReferenceEquals(clone.TheString, source.TheString).Should().BeFalse();

            clone.TheDateTime.Should().Be(source.TheDateTime);
            clone.TheDecimal.Should().Be(source.TheDecimal);
            clone.TheBool.Should().Be(source.TheBool);

            if (clone.TheIntsArray.HasValues() && source.TheIntsArray.HasValues())
                clone.TheIntsArray.ContainsAll(source.TheIntsArray).Should().BeTrue();
            if (clone.TheIntsList.HasValues() && source.TheIntsList.HasValues())
                clone.TheIntsList.ContainsAll(source.TheIntsList).Should().BeTrue();    

            if (clone.TheCloneClassList != null && source.TheCloneClassList != null)
                for (int i = 0; i < source.TheCloneClassList.Count; i++)
                    AssertCloneProperties(clone.TheCloneClassList[i], source.TheCloneClassList[i]);
        }

        private void AssertStructProperties(StructObj clone, StructObj source)
        {
            clone.TheInt.Should().Be(source.TheInt);

            clone.TheString.Should().Be(source.TheString);

            clone.TheDateTime.Should().Be(source.TheDateTime);
            clone.TheDecimal.Should().Be(source.TheDecimal);
            clone.TheBool.Should().Be(source.TheBool);
        }

    }

    public class CloneClass
    {
        public CloneClass() { }
        public CloneClass(int theInt, string theString, DateTime theDateTime, bool theBool, decimal theDecimal, Type theType)
        {
            TheInt = theInt;
            TheString = theString;
            TheDateTime = theDateTime;
            TheBool = theBool;
            TheDecimal = theDecimal;
            TheType = theType;
        }

        public int TheInt { get; set; }
        public int[] TheIntsArray { get; set; }
        public List<int> TheIntsList { get; set; }
        public List<CloneClass> TheCloneClassList { get; set; }
        public string TheString { get; set; }
        public DateTime TheDateTime { get; set; }
        public bool TheBool { get; set; }
        public decimal TheDecimal { get; set; }
        public Type TheType { get; set; }

        public StructObj TheStruct { get; set; }
        public CloneClass Child { get; set; }
    }

    public struct StructObj
    {
        public StructObj(int theInt, string theString, DateTime theDateTime, bool theBool, decimal theDecimal, Type theType)
        {
            TheInt = theInt;
            TheString = theString;
            TheDateTime = theDateTime;
            TheBool = theBool;
            TheDecimal = theDecimal;
            TheType = theType;
        }

        public int TheInt;
        public string TheString;
        public DateTime TheDateTime;
        public bool TheBool;
        public decimal TheDecimal;
        public Type TheType;
    }
}
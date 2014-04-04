using System;
using FluentAssertions;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DateTimeExtensionTests
    {
        [Test]
        public void IsMonday_ShouldWork()
        {
            Dates.MondayDate.IsMonday().Should().BeTrue();
            Dates.TuesdayDate.IsMonday().Should().BeFalse();
            Dates.WednesdayDate.IsMonday().Should().BeFalse();
            Dates.ThursdayDate.IsMonday().Should().BeFalse();
            Dates.FridayDate.IsMonday().Should().BeFalse();
            Dates.SaturdayDate.IsMonday().Should().BeFalse();
            Dates.SundayDate.IsMonday().Should().BeFalse();
        }

        [Test]
        public void IsTuesday_ShouldWork()
        {
            Dates.MondayDate.IsTuesday().Should().BeFalse();
            Dates.TuesdayDate.IsTuesday().Should().BeTrue();
            Dates.WednesdayDate.IsTuesday().Should().BeFalse();
            Dates.ThursdayDate.IsTuesday().Should().BeFalse();
            Dates.FridayDate.IsTuesday().Should().BeFalse();
            Dates.SaturdayDate.IsTuesday().Should().BeFalse();
            Dates.SundayDate.IsTuesday().Should().BeFalse();
        }

        [Test]
        public void IsWednesday_ShouldWork()
        {
            Dates.MondayDate.IsWednesday().Should().BeFalse();
            Dates.TuesdayDate.IsWednesday().Should().BeFalse();
            Dates.WednesdayDate.IsWednesday().Should().BeTrue();
            Dates.ThursdayDate.IsWednesday().Should().BeFalse();
            Dates.FridayDate.IsWednesday().Should().BeFalse();
            Dates.SaturdayDate.IsWednesday().Should().BeFalse();
            Dates.SundayDate.IsWednesday().Should().BeFalse();
        }

        [Test]
        public void IsThursday_ShouldWork()
        {
            Dates.MondayDate.IsThursday().Should().BeFalse();
            Dates.TuesdayDate.IsThursday().Should().BeFalse();
            Dates.WednesdayDate.IsThursday().Should().BeFalse();
            Dates.ThursdayDate.IsThursday().Should().BeTrue();
            Dates.FridayDate.IsThursday().Should().BeFalse();
            Dates.SaturdayDate.IsThursday().Should().BeFalse();
            Dates.SundayDate.IsThursday().Should().BeFalse();
        }

        [Test]
        public void IsFriday_ShouldWork()
        {
            Dates.MondayDate.IsFriday().Should().BeFalse();
            Dates.TuesdayDate.IsFriday().Should().BeFalse();
            Dates.WednesdayDate.IsFriday().Should().BeFalse();
            Dates.ThursdayDate.IsFriday().Should().BeFalse();
            Dates.FridayDate.IsFriday().Should().BeTrue();
            Dates.SaturdayDate.IsFriday().Should().BeFalse();
            Dates.SundayDate.IsFriday().Should().BeFalse();
        }

        [Test]
        public void IsSaturday_ShouldWork()
        {
            Dates.MondayDate.IsSaturday().Should().BeFalse();
            Dates.TuesdayDate.IsSaturday().Should().BeFalse();
            Dates.WednesdayDate.IsSaturday().Should().BeFalse();
            Dates.ThursdayDate.IsSaturday().Should().BeFalse();
            Dates.FridayDate.IsSaturday().Should().BeFalse();
            Dates.SaturdayDate.IsSaturday().Should().BeTrue();
            Dates.SundayDate.IsSaturday().Should().BeFalse();
        }

        [Test]
        public void IsSunday_ShouldWork()
        {
            Dates.MondayDate.IsSunday().Should().BeFalse();
            Dates.TuesdayDate.IsSunday().Should().BeFalse();
            Dates.WednesdayDate.IsSunday().Should().BeFalse();
            Dates.ThursdayDate.IsSunday().Should().BeFalse();
            Dates.FridayDate.IsSunday().Should().BeFalse();
            Dates.SaturdayDate.IsSunday().Should().BeFalse();
            Dates.SundayDate.IsSunday().Should().BeTrue();
        }

        [Test]
        public void IsWeekday_ShouldWork()
        {
            Dates.MondayDate.IsWeekday().Should().BeTrue();
            Dates.TuesdayDate.IsWeekday().Should().BeTrue();
            Dates.WednesdayDate.IsWeekday().Should().BeTrue();
            Dates.ThursdayDate.IsWeekday().Should().BeTrue();
            Dates.FridayDate.IsWeekday().Should().BeTrue();
            Dates.SaturdayDate.IsWeekday().Should().BeFalse();
            Dates.SundayDate.IsWeekday().Should().BeFalse();
        }

        [Test]
        public void IsWeekend_ShouldWork()
        {
            Dates.MondayDate.IsWeekend().Should().BeFalse();
            Dates.TuesdayDate.IsWeekend().Should().BeFalse();
            Dates.WednesdayDate.IsWeekend().Should().BeFalse();
            Dates.ThursdayDate.IsWeekend().Should().BeFalse();
            Dates.FridayDate.IsWeekend().Should().BeFalse();
            Dates.SaturdayDate.IsWeekend().Should().BeTrue();
            Dates.SundayDate.IsWeekend().Should().BeTrue();
        }

    }

    public static class Dates
    {
        public static DateTime MondayDate { get { return new DateTime(2014, 4, 7); } }
        public static DateTime TuesdayDate { get { return new DateTime(2014, 4, 8); } }
        public static DateTime WednesdayDate { get { return new DateTime(2014, 4, 9); } }
        public static DateTime ThursdayDate { get { return new DateTime(2014, 4, 10); } }
        public static DateTime FridayDate { get { return new DateTime(2014, 4, 11); } }
        public static DateTime SaturdayDate { get { return new DateTime(2014, 4, 12); } }
        public static DateTime SundayDate { get { return new DateTime(2014, 4, 13); } }
    }
}
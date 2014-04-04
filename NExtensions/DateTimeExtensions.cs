using System;

namespace NExtensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Saturday || input.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(this DateTime input)
        {
            return !input.IsWeekend();
        }

        public static bool IsMonday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Monday;
        }

        public static bool IsTuesday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Tuesday;
        }

        public static bool IsWednesday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Wednesday;
        }

        public static bool IsThursday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Thursday;
        }

        public static bool IsFriday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Friday;
        }

        public static bool IsSaturday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsSunday(this DateTime input)
        {
            return input.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
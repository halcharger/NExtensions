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

        public static DateTime LastDayOfMonth(this DateTime input)
        {
            return input.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfMonth(this DateTime input)
        {
            return new DateTime(input.Year, input.Month, 1);
        }

        public static bool IsLastDayOfMonth(this DateTime input)
        {
            return input.Date == input.LastDayOfMonth();
        }

        public static bool IsFirstDayOfMonth(this DateTime input)
        {
            return input.Day == 1;
        }

        public static DateTime AddWeekDays(this DateTime date, int weekDays)
        {
            var direction = weekDays < 0 ? -1 : 1;
            var newDate = date;
            while (weekDays != 0)
            {
                newDate = newDate.AddDays(direction);
                if (newDate.IsWeekday())
                {
                    weekDays -= direction;
                }
            }
            
            return newDate;
        }
    }
}
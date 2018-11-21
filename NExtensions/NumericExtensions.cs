using System;

namespace NExtensions
{
    public static class NumericExtensions
    {
        public static decimal Absolute(this decimal input)
        {
            return Math.Abs(input);
        }

        public static int Absolute(this int input)
        {
            return Math.Abs(input);
        }

        public static decimal RoundToMoney(this decimal val)
        {
            return Math.Round(val, 2);
        }

        public static decimal RoundTo(this decimal val, int place)
        {
            return Math.Round(val, place);
        }
    }
}
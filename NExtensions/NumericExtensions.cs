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
    }
}
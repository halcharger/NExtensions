using System;
using System.Collections;

namespace NExtensions
{
    public static class TypeExtensions
    {
        public static bool IsString(this Type input)
        {
            return input == typeof (string);
        }

        public static bool IsType(this Type input)
        {
            return input == typeof (Type);
        }

        public static bool IsIEnumerable(this Type input)
        {
            return typeof (IEnumerable).IsAssignableFrom(input);
        }

        public static bool IsIList(this Type input)
        {
            return typeof(IList).IsAssignableFrom(input);
        }

        public static object CreateInstance(this Type input)
        {
            return Activator.CreateInstance(input);
        }

        public static T CreateInstance<T>(this Type input)
        {
            return (T)Activator.CreateInstance(input);
        }

    }
}
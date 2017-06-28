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

        public static bool IsDecimal(this Type input)
        {
            return input == typeof(decimal);
        }

        public static bool IsInt(this Type input)
        {
            return input == typeof(int);
        }

        public static bool IsDateTime(this Type input)
        {
            return input == typeof(DateTime);
        }

        public static bool IsBool(this Type input)
        {
            return input == typeof(bool);
        }

        public static bool IsNullableDecimal(this Type input)
        {
            return input == typeof(decimal?);
        }

        public static bool IsNullableInt(this Type input)
        {
            return input == typeof(int?);
        }

        public static bool IsNullableDateTime(this Type input)
        {
            return input == typeof(DateTime?);
        }

        public static bool IsNullableBool(this Type input)
        {
            return input == typeof(bool?);
        }

        public static bool IsType(this Type input)
        {
            return input == typeof (Type);
        }

        //public static bool IsIEnumerable(this Type input)
        //{
        //    return typeof (IEnumerable).IsAssignableFrom(input);
        //}

        //public static bool IsIList(this Type input)
        //{
        //    return typeof(IList).IsAssignableFrom(input);
        //}

        public static bool IsNotString(this Type input)
        {
            return input != typeof(string);
        }

        public static bool IsNotDecimal(this Type input)
        {
            return input != typeof(decimal);
        }

        public static bool IsNotInt(this Type input)
        {
            return input != typeof(int);
        }

        public static bool IsNotDateTime(this Type input)
        {
            return input != typeof(DateTime);
        }

        public static bool IsNotBool(this Type input)
        {
            return input != typeof(bool);
        }

        public static bool IsNotNullableDecimal(this Type input)
        {
            return input != typeof(decimal?);
        }

        public static bool IsNotNullableInt(this Type input)
        {
            return input != typeof(int?);
        }

        public static bool IsNotNullableDateTime(this Type input)
        {
            return input != typeof(DateTime?);
        }

        public static bool IsNotNullableBool(this Type input)
        {
            return input != typeof(bool?);
        }

        public static bool IsNotType(this Type input)
        {
            return input != typeof(Type);
        }

        //public static bool IsNotIEnumerable(this Type input)
        //{
        //    return !typeof(IEnumerable).IsAssignableFrom(input);
        //}

        //public static bool IsNotIList(this Type input)
        //{
        //    return !typeof(IList).IsAssignableFrom(input);
        //}

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
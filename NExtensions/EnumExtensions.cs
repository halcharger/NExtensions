using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NExtensions
{
    public static class Enums
    {
        public static string GetDescription<T>(this T value) where T : struct
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Value must be of Enum type.");
            }

            var fi = type.GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        public static IEnumerable<T> GetValues<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        public static IEnumerable<T> GetValues<T>(this Type type) where T : struct
        {
            return GetValues<T>();
        }

        public static T ToEnum<T>(this string input) where T : struct
        {
            var values = GetValues<T>().Where(x => x.ToString().Equals(input, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (values.Any()) return values.Single();

            var descriptions = GetValues<T>().Where(x => x.GetDescription().Equals(input, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (descriptions.Any()) return descriptions.Single();

            throw new ArgumentException("Cannot find enum value '{0}' for type {1}".FormatWith(input ?? "<NULL>", typeof(T).Name));
        }
    }
}
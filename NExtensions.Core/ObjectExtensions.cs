using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NExtensions.Core
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns a string representation of an object even if it is null.
        /// </summary>
        /// <param name="input">The object on which ToString will be called if it is not null</param>
        /// <param name="defaultIfNull">The value to be returned if <param name="input" /> is null. This is by default String.Empty</param>
        /// <returns></returns>
        public static string ToNullSafeString(this object input, string defaultIfNull = "")
        {
            return input == null ? defaultIfNull : input.ToString();
        }

        public static IEnumerable<PropertyInfo> GetProperties(this object input)
        {
            return input.GetType().GetProperties();
        }

        public static object GetValueForProperty(this object input, string propertyName, object defaultValue = null)
        {
            if (input == null) return null;

            var propInfo = input.GetType().GetTypeInfo().GetProperty(propertyName);

            return propInfo == null ? defaultValue : propInfo.GetValue(input);
        }

        public static void SetValueForProperty(this object input, string propertyName, object value)
        {
            if (input == null) return;

            var propInfo = input.GetType().GetTypeInfo().GetProperty(propertyName);

            if (propInfo == null) return;

            propInfo.SetValue(input, value);
        }

        public static void ThrowIfNull(this object input, string paramName)
        {
            if (input == null)
                throw new ArgumentNullException(paramName);
        }

        public static bool NotNull(this object input)
        {
            return !input.IsNull();
        }

        public static bool IsNull(this object input)
        {
            return input == null;
        }

        public static Task<T> ToTask<T>(this T input)
        {
            return Task.FromResult(input);
        }
    }
}
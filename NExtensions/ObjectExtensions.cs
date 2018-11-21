using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NExtensions
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

        /// <summary>
        /// Makes a deep copy from an object using only the public properties.
        /// Doesn't copy the reference memory, only data.
        /// </summary>
        /// <typeparam name="T">Type of the return object.</typeparam>
        /// <param name="source">Object to be cloned.</param>
        /// <returns>Returns the cloned object.</returns>
        public static T Clone<T>(this T source) where T : class, new()
        {
            switch (source)
            {
                case null:
                    return null;
                case ValueType _:
                    return source;
                case ICloneable cloneable:
                    return (T) cloneable.Clone();
            }

            var clone = source.GetType().CreateInstance<T>();

            clone.RecursivelySetPropertyValues(source);

            return clone;
        }

        public static IEnumerable<PropertyInfo> GetProperties(this object input)
        {
            return input.GetType().GetProperties();
        }

        public static object GetValueForProperty(this object input, string propertyName, object defaultValue = null)
        {
            if (input == null)
            {
                return null;
            }

            var propInfo = input.GetType().GetProperty(propertyName);

            return propInfo == null ? defaultValue : propInfo.GetValue(input);
        }

        public static void SetValueForProperty(this object input, string propertyName, object value)
        {
            if (input == null)
            {
                return;
            }

            var propInfo = input.GetType().GetProperty(propertyName);

            if (propInfo == null)
            {
                return;
            }

            propInfo.SetValue(input, value);
        }

        public static void ThrowIfNull(this object input, string paramName)
        {
            input.ThrowIfNull<ArgumentNullException>(paramName);
        }

        public static void ThrowIfNull<T>(this object input, string message) where T : Exception, new()
        {
            if (input.NotNull())
            {
                return;
            }

            var exception = typeof(T).CreateInstance<T>(message);
            throw exception;
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

        private static void RecursivelySetPropertyValues<T>(this T clone, T source) where T : class
        {
            if (clone == null || source == null)
            {
                return;
            }

            foreach (var p in source.GetProperties())
                if (p.PropertyType.IsValueType ||
                    p.PropertyType.IsType())
                {
                    //basic framework types or structs
                    p.SetValue(clone, p.GetValue(source));
                }
                else if (p.PropertyType.IsString())
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    p.SetValue(clone, p.GetValue(source).ToNullSafeString(null).Copy());
                }
                else if (p.PropertyType.IsIEnumerable())
                {
                    if (p.PropertyType.IsArray)
                    {
                        var sourceArray = (Array) p.GetValue(source);
                        if (sourceArray != null)
                        {
                            var newArray =
                                CopyArrayValues(
                                    Array.CreateInstance(p.PropertyType.GetElementType(), sourceArray.Length),
                                    sourceArray);
                            p.SetValue(clone, newArray);
                        }
                    }
                    else if (p.PropertyType.IsIList())
                    {
                        var sourceList = (IList) p.GetValue(source);
                        if (sourceList != null)
                        {
                            var newList = CopyListValues(p.PropertyType.CreateInstance<IList>(), sourceList);
                            p.SetValue(clone, newList);
                        }
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    //complex types (classes)
                    p.SetValue(clone, p.PropertyType.CreateInstance());
                    RecursivelySetPropertyValues(p.GetValue(clone), p.GetValue(source));
                }
        }

        private static Array CopyArrayValues(Array input, IEnumerable source)
        {
            source
                .ForEach((index, item) =>
                {
                    var type = item.GetType();
                    if (type.IsValueType || type.IsType())
                    {
                        input.SetValue(item, index);
                    }
                    else if (type.IsString())
                    {
                        input.SetValue(item.ToNullSafeString().Copy(), index);
                    }
                    else if (type.IsClass)
                    {
                        input.SetValue(item.Clone(), index);
                    }
                });

            return input;
        }

        private static IList CopyListValues(IList input, IEnumerable source)
        {
            var groups = source
                .Cast<object>()
                .ToLookup(i => i.GetType(), i => i);

            input.AddRange(
                groups
                    .Where(i => i.Key.IsValueType || i.Key.IsType())
                    .SelectMany(i => i)
            );

            input.AddRange(
                groups
                    .Where(i => i.Key.IsString())
                    .SelectMany(i => i)
                    .Select(i => i.ToNullSafeString().Copy())
            );

            input.AddRange(
                groups
                    .Where(i => i.Key.IsClass)
                    .SelectMany(i => i)
                    .Select(i => i.Clone())
            );

            return input;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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
            if (source == null) return null;
            if (source is ValueType) return source;
            if (source is ICloneable) return (T) ((ICloneable) source).Clone();

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
            if (input == null) return null;

            var propInfo = input.GetType().GetProperty(propertyName);

            return propInfo == null ? defaultValue : propInfo.GetValue(input);
        }

        public static void SetValueForProperty(this object input, string propertyName, object value)
        {
            if (input == null) return;

            var propInfo = input.GetType().GetProperty(propertyName);

            if (propInfo == null) return;

            propInfo.SetValue(input, value);
        }

        public static void ThrowIfNull(this object input, string paramName)
        {
            if (input == null)
                throw new ArgumentNullException(paramName);
        }

        private static void RecursivelySetPropertyValues<T>(this T clone, T source) where T : class
        {
            if (clone == null || source == null) return;

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
                        var sourceArray = (Array)p.GetValue(source);
                        if (sourceArray != null)
                        {
                            var newArray = CopyArrayValues(Array.CreateInstance(p.PropertyType.GetElementType(), sourceArray.Length), sourceArray);
                            p.SetValue(clone, newArray);
                        }
                    }
                    else if (p.PropertyType.IsIList())
                    {
                        var sourceList = (IList)p.GetValue(source);
                        if (sourceList != null)
                        {
                            var newList = CopyListvalues(p.PropertyType.CreateInstance<IList>(), sourceList);
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

        private static Array CopyArrayValues(Array input, Array source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                var item = source.GetValue(i);
                var type = item.GetType();
                if (type.IsValueType ||
                    type.IsType())
                {
                    input.SetValue(item, i);
                }
                else if (type.IsString())
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    input.SetValue(item.ToNullSafeString().Copy(), i);
                }
                else if (type.IsClass)
                {
                    input.SetValue(item.Clone(), i);
                }

            }

            return input;
        }

        private static IList CopyListvalues(IList input, IList source)
        {
            foreach (var item in source)
            {
                var type = item.GetType();
                if (type.IsValueType ||
                    type.IsType())
                {
                    input.Add(item);
                }
                else if (type.IsString())
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    input.Add(item.ToNullSafeString().Copy());
                }
                else if (type.IsClass)
                {
                    input.Add(item.Clone());
                }
            }

            return input;
        }
    }
}
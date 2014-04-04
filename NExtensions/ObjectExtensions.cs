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

            var clone = (T)Activator.CreateInstance(source.GetType());

            clone.RecursivelySetPropertyValues(source);

            return clone;
        }

        public static IEnumerable<PropertyInfo> GetProperties(this object input)
        {
            return input.GetType().GetProperties();
        }

        private static void RecursivelySetPropertyValues<T>(this T clone, T source) where T : class
        {
            if (clone == null || source == null) return;

            foreach (var p in source.GetProperties())
                if (p.PropertyType.IsPrimitive || 
                    p.PropertyType.IsValueType ||
                    p.PropertyType == typeof(Type))
                {
                    //basic framework types or structs
                    p.SetValue(clone, p.GetValue(source));
                }
                else if (p.PropertyType == typeof (string))
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    var newStringVal = (string)p.GetValue(source);
                    p.SetValue(clone, string.Copy(newStringVal));
                }
                else if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
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
                    else if (typeof (IList).IsAssignableFrom(p.PropertyType))
                    {
                        var sourceList = (IList)p.GetValue(source);
                        if (sourceList != null)
                        {
                            var newList = CopyListvalues((IList)Activator.CreateInstance(p.PropertyType), sourceList);
                            p.SetValue(clone, newList);
                        }
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    //complex types (classes)
                    p.SetValue(clone, Activator.CreateInstance(p.PropertyType));
                    RecursivelySetPropertyValues(p.GetValue(clone), p.GetValue(source));
                }
        }

        private static Array CopyArrayValues(Array input, Array source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                var item = source.GetValue(i);
                var type = item.GetType();
                if (type.IsPrimitive ||
                    type.IsValueType ||
                    type == typeof(Type))
                {
                    input.SetValue(item, i);
                }
                else if (type == typeof(string))
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    input.SetValue(string.Copy((string)item), i);
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
                if (type.IsPrimitive ||
                    type.IsValueType ||
                    type == typeof (Type))
                {
                    input.Add(item);
                }
                else if (type == typeof (string))
                {
                    //strings, we don't want a ref to the string value, we want a copy
                    input.Add(string.Copy((string)item));
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
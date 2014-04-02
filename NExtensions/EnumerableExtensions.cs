using System;
using System.Collections.Generic;
using System.Linq;

namespace NExtensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the given Action<T> for each item in the enumerable
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// 
        /// <returns>
        /// false if the enumerable sequence contains any elements; otherwise, true.
        /// </returns>
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// 
        /// <returns>
        /// false if any elements in the enumerable sequence pass the test in the specified predicate; otherwise, true.
        /// </returns>
        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable == null || !enumerable.Any(predicate);
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        {
            return collection ?? Enumerable.Empty<T>();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.None();
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            return collection.ContainsAll(otherCollection.ToArray());
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            return otherCollection.All(collection.Contains);
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            return collection.ContainsNone(otherCollection.ToArray());
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            return otherCollection.None(collection.Contains);
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}
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

        public static bool HasValues<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.IsNullOrEmpty();
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            if (collection == null) return false;
            return collection.ContainsAll(otherCollection.ToArray());
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            if (collection == null) return false;
            return otherCollection.All(collection.Contains);
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            if (otherCollection == null) return true;
            return collection.ContainsNone(otherCollection.ToArray());
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            if (collection == null) return false;
            return otherCollection.None(collection.Contains);
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> collection)
        {
            return collection.EmptyIfNull().GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key);
        }

        public static IEnumerable<IGrouping<TKey, T>> GetDuplicates<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            return collection.EmptyIfNull().GroupBy(keySelector).Where(x => x.Count() > 1);
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NExtensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the given <see cref="Action{T}"/> for each item in the enumerable
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable.IsNull())
            {
                return;
            }

            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<int, T> action)
        {
            var items = enumerable.ToSafeEnumeration();
            if (items.IsNull())
            {
                return;
            }

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                action(i, item);
            }
        }

        public static void ForEach(this IEnumerable enumerable, Action<object> action)
        {
            enumerable.Cast<object>().ForEach(action);
        }

        public static void ForEach(this IEnumerable enumerable, Action<int, object> action)
        {
            enumerable.Cast<object>().ForEach(action);
        }

        public static T[] ToSafeEnumeration<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.IsNull())
            {
                return null;
            }

            return enumerable as T[] ?? enumerable.ToArray();
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
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
            return collection != null && collection.ContainsAll(otherCollection.ToArray());
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            return collection != null && otherCollection.All(collection.Contains);
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            return otherCollection == null || collection.ContainsNone(otherCollection.ToArray());
        }

        public static bool ContainsNone<T>(this IEnumerable<T> collection, params T[] otherCollection)
        {
            return collection != null && otherCollection.None(collection.Contains);
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> collection)
        {
            return collection.EmptyIfNull().GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key);
        }

        public static IEnumerable<IGrouping<TKey, T>> GetDuplicates<T, TKey>(this IEnumerable<T> collection,
            Func<T, TKey> keySelector)
        {
            return collection.EmptyIfNull().GroupBy(keySelector).Where(x => x.Count() > 1);
        }
    }
}
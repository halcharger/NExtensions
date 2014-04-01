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
        /// Inverse of IEnumerable<T>.Any()
        /// </summary>
        public static bool Empty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
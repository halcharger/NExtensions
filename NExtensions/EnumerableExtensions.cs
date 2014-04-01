using System;
using System.Collections.Generic;

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
    }
}
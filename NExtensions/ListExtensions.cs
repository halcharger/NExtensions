// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Procure Software Development">
//     Copyright (c) 2018 Procure Software Development
// </copyright>
// <author></author>
// <summary>
//     
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NExtensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList list, IEnumerable<T> itemsToAdd)
        {
            var toAdd = itemsToAdd as T[] ?? itemsToAdd.ToArray();
            if (toAdd.IsNullOrEmpty())
            {
                return;
            }

            if (list.IsNull())
            {
                return;
            }
            
            toAdd.ForEach(i => list.Add(i));
        }

        public static void AddRange(this IList list, IEnumerable itemsToAdd)
        {
            list.AddRange(itemsToAdd.Cast<object>());
        }
    }
}
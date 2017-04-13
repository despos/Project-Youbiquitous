///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace Expoware.Youbiquitous.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks whether the list object is null or empty
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="theList">List object</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> theList)
        {
            return theList == null || theList.Count == 0;
        }

        /// <summary>
        /// DistinctBy function on Enumerable objects
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}
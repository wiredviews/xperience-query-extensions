using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XperienceCommunity.QueryExtensions.Collections
{
    public static class XperienceCommunityCollectionExtensions
    {
        /// <summary>
        /// Executes the <paramref name="action" /> for each item in the sequence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<TSource>> TapAsync<TSource>(this Task<IEnumerable<TSource>> source, Action<TSource> action)
            where TSource : class
        {
            var results = await source;

            foreach (var item in results)
            {
                action(item);
            }

            return results;
        }

        /// <summary>
        /// Tranforms each item in the sequence using the <paramref name="projection" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<TReturn>> SelectAsync<TSource, TReturn>(this Task<IEnumerable<TSource>> source, Func<TSource, TReturn> projection)
        {
            var results = await source;

            return results.Select(projection);
        }
    }
}

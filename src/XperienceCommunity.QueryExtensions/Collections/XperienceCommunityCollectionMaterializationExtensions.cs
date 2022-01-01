using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XperienceCommunity.QueryExtensions.Collections
{
    public static class XperienceCommunityCollectionMaterializationExtensions
    {
        /// <summary>
        /// Returns the first object in the sequence and null if the sequence is empty
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static async Task<TSource?> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
            where TSource : class
        {
            var results = await source;

            return results.FirstOrDefault();
        }

        /// <summary>
        /// Returns the first object in the sequence that matches the <paramref name="predicate" /> and null if there is no match
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static async Task<TSource?> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source, Func<TSource, bool> predicate)
            where TSource : class
        {
            var results = await source;

            return results.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Materializes the sequence into a List
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static async Task<IList<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            var results = await source;

            return results.ToList();
        }

        /// <summary>
        /// Materializes the sequence into an Array
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static async Task<TSource[]> ToArrayAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            var results = await source;

            return results.ToArray();
        }

        /// <summary>
        /// Maps the collection into a <typeparamref name="TReturn" /> using the given <paramref name="projection" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public static TReturn MapCollection<TSource, TReturn>(
            this IEnumerable<TSource> source, Func<IEnumerable<TSource>, TReturn> projection) =>
            projection(source);

        /// <summary>
        /// Maps the collection into a <typeparamref name="TReturn" /> using the given <paramref name="projection" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>

        public static async Task<TReturn> MapCollectionAsync<TSource, TReturn>(
            this Task<IEnumerable<TSource>> source, Func<IEnumerable<TSource>, TReturn> projection)
        {
            var results = await source;

            return projection(results);
        }
    }
}

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
    }
}

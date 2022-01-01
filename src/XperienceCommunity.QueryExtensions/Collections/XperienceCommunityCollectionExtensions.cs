using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XperienceCommunity.QueryExtensions.Collections
{
    public static class XperienceCommunityCollectionExtensions
    {
        public static async Task<TSource?> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
            where TSource : class
        {
            var results = await source;

            return results.FirstOrDefault();
        }

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

        public static async Task<IEnumerable<TReturn>> SelectAsync<TSource, TReturn>(this Task<IEnumerable<TSource>> source, Func<TSource, TReturn> projection)
        {
            var results = await source;

            return results.Select(projection);
        }

        public static async Task<IList<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            var results = await source;

            return results.ToList();
        }

        public static async Task<TSource[]> ToArrayAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            var results = await source;

            return results.ToArray();
        }

        public static TReturn MapCollection<TSource, TReturn>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, TReturn> projection)
        {
            return projection(source);
        }

        public static async Task<TReturn> MapCollectionAsync<TSource, TReturn>(this Task<IEnumerable<TSource>> source, Func<IEnumerable<TSource>, TReturn> projection)
        {
            var results = await source;

            return projection(results);
        }
    }
}

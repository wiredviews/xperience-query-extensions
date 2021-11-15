using System.Linq;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace System.Collections.Generic
{
    public static class XperienceCommunityCollectionExtensions
    {
        public static async Task<TPage?> FirstOrDefaultAsync<TPage>(this Task<IEnumerable<TPage>> results)
            where TPage : TreeNode, new()
        {
            var pages = await results;

            return pages.FirstOrDefault();
        }

        public static async Task<IEnumerable<TReturn>> SelectAsync<TSource, TReturn>(this Task<IEnumerable<TSource>> source, Func<TSource, TReturn> projection)
        {
            var results = await source;

            return results.Select(projection);
        }

        public static async Task<IEnumerable<TSource>> WhereAsync<TSource>(this Task<IEnumerable<TSource>> source, Func<TSource, bool> predicate)
        {
            var results = await source;

            return results.Where(predicate);
        }
    }
}

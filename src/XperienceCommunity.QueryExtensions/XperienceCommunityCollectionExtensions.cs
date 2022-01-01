using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace XperienceCommunity.QueryExtensions.Collections
{
    public static class XperienceCommunityCollectionExtensions
    {
        public static async Task<TPage?> FirstOrDefaultAsync<TPage>(this Task<IEnumerable<TPage>> results)
            where TPage : TreeNode, new()
        {
            var pages = await results;

            return pages.FirstOrDefault();
        }

        public static async Task<TPage?> SingleAsync<TPage>(this Task<IEnumerable<TPage>> results)
            where TPage : TreeNode, new()
        {
            var pages = await results;

            return pages.Single();
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
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace Kentico.Content.Web.Mvc
{
    public static class XperienceCommunityPageRetrieverExtensions
    {
        /// <summary>
        /// Uses the <see cref="IPageRetriever" /> to return a paged set of results according to the values of the paging parameters.
        /// </summary>
        /// <param name="retreiever"></param>
        /// <param name="pageIndex">0 based index, normalized to a value in the range 0 &lt;= pageIndex &lt;= int.MaxValue</param>
        /// <param name="pageSize">normalized to a value in the range 1 &lt;= pageSize &lt;= int.MaxValue</param>
        /// <param name="applyQueryParametersAction"></param>
        /// <param name="buildCacheAction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPageType"></typeparam>
        /// <returns>A tuple of the total number of records in the database, and the paged result set</returns>
        public static async Task<(int TotalRecords, IEnumerable<TPageType> Items)> RetrievePagedAsync<TPageType>(
            this IPageRetriever retreiever,
            int pageIndex,
            int pageSize,
            Action<DocumentQuery<TPageType>>? applyQueryParametersAction = null,
            Action<IPageCacheBuilder<TPageType>>? buildCacheAction = null,
            CancellationToken? cancellationToken = null) where TPageType : TreeNode, new()
        {
            pageIndex = Math.Clamp(pageIndex, 0, int.MaxValue);
            pageSize = Math.Clamp(pageSize, 1, int.MaxValue);

            int totalRecords = 0;

            var result = await retreiever.RetrieveAsync(query =>
            {
                applyQueryParametersAction?.Invoke(query);

                totalRecords = query
                    .Page(pageIndex, pageSize)
                    .TotalRecords;
            }, buildCacheAction, cancellationToken);

            return (totalRecords, result);
        }
    }
}

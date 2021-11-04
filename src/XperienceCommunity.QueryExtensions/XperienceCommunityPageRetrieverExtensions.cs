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
        /// Uses the <see cref="IPageRetriever" /> to return a paged set of results according to the values of the paging parameters
        /// </summary>
        /// <param name="retreiever"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
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

        /// <summary>
        /// Uses the <see cref="IPageRetriever" /> to return a the first item in a query result set, if it exists
        /// </summary>
        /// <param name="retreiever"></param>
        /// <param name="applyQueryParametersAction"></param>
        /// <param name="buildCacheAction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPageType"></typeparam>
        /// <returns>A the first item in the query result set, or null if there were no items</returns>
        public static async Task<TPageType?> FirstOrDefaultAsync<TPageType>(
            this IPageRetriever retreiever,
            Action<DocumentQuery<TPageType>>? applyQueryParametersAction = null,
            Action<IPageCacheBuilder<TPageType>>? buildCacheAction = null,
            CancellationToken? cancellationToken = null) where TPageType : TreeNode, new() =>
                await retreiever
                    .RetrieveAsync(query => applyQueryParametersAction?.Invoke(query), buildCacheAction, cancellationToken)
                    .FirstOrDefaultAsync();

        /// <summary>
        /// Uses the <see cref="IPageRetriever" /> to project the results of a query to a new type
        /// </summary>
        /// <param name="retreiever"></param>
        /// <param name="projection"></param>
        /// <param name="applyQueryParametersAction"></param>
        /// <param name="buildCacheAction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPageType"></typeparam>
        /// <typeparam name="TNewType"></typeparam>
        /// <returns>A the first item in the query result set, or null if there were no items</returns>
        public static async Task<IEnumerable<TNewType>> SelectAsync<TPageType, TNewType>(
            this IPageRetriever retreiever,
            Func<TPageType, TNewType> projection,
            Action<DocumentQuery<TPageType>>? applyQueryParametersAction = null,
            Action<IPageCacheBuilder<TPageType>>? buildCacheAction = null,
            CancellationToken? cancellationToken = null) where TPageType : TreeNode, new() =>
                await retreiever
                    .RetrieveAsync(query => applyQueryParametersAction?.Invoke(query), buildCacheAction, cancellationToken)
                    .SelectAsync(projection);
    }
}

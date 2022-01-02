using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace XperienceCommunity.QueryExtensions.Documents
{
    public static class XperienceCommunityDocumentMaterializationExtensions
    {
        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{TDocument}"/> of the generic Page type
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<TDocument>> ToListAsync<TDocument>(this DocumentQuery<TDocument> query, CancellationToken token = default)
            where TDocument : TreeNode, new()
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.ToList();
        }

        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{TDocument}"/> of the generic Page type
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="projection">Mapping function from <typeparamref name="TDocument" /> to <typeparamref name="TReturn" /></param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<TReturn>> ToListAsync<TDocument, TReturn>(
            this DocumentQuery<TDocument> query,
            Func<TDocument, TReturn> projection,
            CancellationToken token = default)
            where TDocument : TreeNode, new()
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.Select(projection).ToList();
        }

        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{T}"/> of <see cref="TreeNode"/>
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<TreeNode>> ToListAsync(this MultiDocumentQuery query, CancellationToken token = default)
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.ToList();
        }

        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{T}"/> of <see cref="TreeNode"/>
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="projection">Mapping function from <see cref="TreeNode"/> to <typeparamref name="TReturn" /></param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<TReturn>> ToListAsync<TReturn>(
            this MultiDocumentQuery query,
            Func<TreeNode, TReturn> projection,
            CancellationToken token = default)
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.Select(projection).ToList();
        }

        /// <summary>
        /// Returns the first item of the <paramref name="query"/> as the generic Page type and null if no items were returned.
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<TDocument?> FirstOrDefaultAsync<TDocument>(this DocumentQuery<TDocument> query, CancellationToken token = default)
            where TDocument : TreeNode, new()
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the first item of the <paramref name="query"/> as the generic Page type and null if no items were returned.
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="projection">Mapping function from <typeparamref name="TDocument" /> to <typeparamref name="TReturn" /></param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<TReturn?> FirstOrDefaultAsync<TDocument, TReturn>(
            this DocumentQuery<TDocument> query,
            Func<TDocument, TReturn> projection,
            CancellationToken token = default)
            where TDocument : TreeNode, new()
            where TReturn : class
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result?.Select(projection).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first item of the <paramref name="query"/> as a <see cref="TreeNode"/> and null if no items were returned.
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<TreeNode?> FirstOrDefaultAsync(this MultiDocumentQuery query, CancellationToken token = default)
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result?.FirstOrDefault();
        }
    }
}

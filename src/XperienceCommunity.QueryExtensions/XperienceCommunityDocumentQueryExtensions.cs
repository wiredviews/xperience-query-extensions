using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;
using Microsoft.Extensions.Logging;

namespace XperienceCommunity.QueryExtensions.Documents
{
    public static class XperienceCommunityDocumentQueryExtensions
    {
        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeGUID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeGuid">Value of the <see cref="TreeNode.NodeGUID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereNodeGUIDEquals<TNode>(this DocumentQuery<TNode> query, Guid nodeGuid) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.NodeGUID), nodeGuid);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeID">Value of the <see cref="TreeNode.NodeID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereNodeIDEquals<TNode>(this DocumentQuery<TNode> query, int nodeID) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.NodeID), nodeID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.DocumentID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="documentID">Value of the <see cref="TreeNode.DocumentID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereDocumentIDEquals<TNode>(this DocumentQuery<TNode> query, int documentID) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.DocumentID), documentID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> ordered by <see cref="TreeNode.NodeOrder"/>
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> OrderByNodeOrder<TNode>(this DocumentQuery<TNode> query) where TNode : TreeNode, new() =>
            query.OrderBy(nameof(TreeNode.NodeOrder));

        /// <summary>
        /// Prints the provided query's full materialized query text using <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, name of the generic will be used.
        /// </param>
        /// <example>
        /// DocumentHelper
        ///     .GetDocuments&lt;TreeNode&gt;()
        ///     .TopN(1)
        ///     .DebugQuery("First Document");
        /// 
        /// 
        /// --- BEGIN [First Document] QUERY ---
        /// 
        /// 
        /// DECLARE @DocumentCulture nvarchar(max) = N'en-US';
        ///
        /// SELECT TOP 1 *
        /// FROM View_CMS_Tree_Joined AS V WITH (NOLOCK, NOEXPAND) LEFT OUTER JOIN COM_SKU AS S WITH (NOLOCK) ON [V].[NodeSKUID] = [S].[SKUID]
        /// WHERE [DocumentCulture] = @DocumentCulture
        ///
        /// 
        /// --- END [First Document] QUERY ---
        /// </example>
        /// <returns></returns>
        public static DocumentQuery<TNode> DebugQuery<TNode>(this DocumentQuery<TNode> query, [CallerFilePath] string queryName = "")
            where TNode : TreeNode, new()
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? typeof(TNode).Name
                : queryName;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"--- BEGIN [{queryName}] QUERY ---");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(query.GetFullQueryText());
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"--- END [{queryName}] QUERY ---");
            Console.WriteLine(Environment.NewLine);

            return query;
        }

        /// <summary>
        /// Allows the caller to specify an action that has access to the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <typeparam name="TNode"></typeparam>
        /// <returns></returns>
        public static DocumentQuery<TNode> Tap<TNode>(this DocumentQuery<TNode> query, Action<DocumentQuery<TNode>> action)
            where TNode : TreeNode, new()
        {
            action(query);

            return query;
        }

        /// <summary>
        /// Allow the caller to specify an action that has access to the full query text at the point
        /// at which this method is called. Useful for custom logging of the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DocumentQuery<TNode> TapQueryText<TNode>(this DocumentQuery<TNode> query, Action<string> action)
            where TNode : TreeNode, new()
        {
            action(query.GetFullQueryText());

            return query;
        }

        /// <summary>
        /// Prints the provided query's full materialized query text using <see cref="LoggerExtensions.LogDebug(ILogger, string, object[])"/>
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="logger">The logger used to output the query</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, name of the generic will be used.
        /// </param>
        /// <returns></returns>
        public static DocumentQuery<TNode> LogQuery<TNode>(this DocumentQuery<TNode> query, ILogger logger, [CallerFilePath] string queryName = "")
            where TNode : TreeNode, new()
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? typeof(TNode).Name
                : queryName;

            logger.LogDebug("{queryName} {queryText}", queryName, query.GetFullQueryText());

            return query;
        }

        /// <summary>
        /// Converts the <paramref name="query"/> to an <see cref="IEnumerable{TReturn}"/> asynchronously
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="projection">Mapping function from <typeparamref name="TDocument" /> to <typeparamref name="TReturn" /></param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TReturn>> SelectAsync<TDocument, TReturn>(
            this DocumentQuery<TDocument> query, 
            Func<TDocument, TReturn> projection,
            CancellationToken token = default)
            where TDocument : TreeNode, new()
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.Select(projection);
        }

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
    }
}

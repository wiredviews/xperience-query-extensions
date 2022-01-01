using System;
using System.Runtime.CompilerServices;
using CMS.DocumentEngine;
using Microsoft.Extensions.Logging;

namespace XperienceCommunity.QueryExtensions.Documents
{
    public static class XperienceCommunityDocumentLoggingExtensions
    {
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
        /// Prints the provided query's full materialized query text using <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, "MultiDocumentQuery" will be used.
        /// </param>
        /// <example>
        /// DocumentHelper
        ///     .GetDocuments()
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
        public static MultiDocumentQuery DebugQuery(this MultiDocumentQuery query, [CallerFilePath] string queryName = "")
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? nameof(MultiDocumentQuery)
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
        /// Allow the caller to specify an action that has access to the full query text at the point
        /// at which this method is called. Useful for custom logging of the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static MultiDocumentQuery TapQueryText<TNode>(this MultiDocumentQuery query, Action<string> action)
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
        /// Prints the provided query's full materialized query text using <see cref="LoggerExtensions.LogDebug(ILogger, string, object[])"/>
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="logger">The logger used to output the query</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, "MultiDocumentQuery" will be used.
        /// </param>
        /// <returns></returns>
        public static MultiDocumentQuery LogQuery(this MultiDocumentQuery query, ILogger logger, [CallerFilePath] string queryName = "")
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? nameof(MultiDocumentQuery)
                : queryName;

            logger.LogDebug("{queryName} {queryText}", queryName, query.GetFullQueryText());

            return query;
        }
    }
}

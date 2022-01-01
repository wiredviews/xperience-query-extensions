using System;
using System.Runtime.CompilerServices;
using CMS.DataEngine;
using Microsoft.Extensions.Logging;

namespace XperienceCommunity.QueryExtensions.Objects
{
    public static class XperienceCommunityObjectQueryLoggingExtensions
    {

        /// <summary>
        /// Prints the provided query's full materialized query text using <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, name of the generic will be used.
        /// </param>
        /// <example>
        /// UserInfo.Provider.Get()
        ///     .WhereEquals("Email", "admin@localhost.local")
        ///     .Print("User");
        /// 
        /// --- BEGIN [User] QUERY ---
        ///
        /// DECLARE @Email nvarchar(max) = N'admin@localhost.local';
        ///
        /// SELECT *
        /// FROM CMS_User
        /// WHERE [Email] = @Email
        ///
        /// --- END [User] QUERY ---
        /// </example>
        /// <returns></returns>
        public static ObjectQuery<TInfo> DebugQuery<TInfo>(this ObjectQuery<TInfo> query, [CallerFilePath] string queryName = "")
            where TInfo : BaseInfo, new()
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? typeof(TInfo).Name
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
        /// at which this method is called
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ObjectQuery<TInfo> TapQueryText<TInfo>(this ObjectQuery<TInfo> query, Action<string> action)
            where TInfo : BaseInfo, new()
        {
            action(query.GetFullQueryText());

            return query;
        }

        /// <summary>
        /// Allow the caller to specify an action that has access to the full query text at the point
        /// at which this method is called
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ObjectQuery TapQueryText(this ObjectQuery query, Action<string> action)
        {
            action(query.GetFullQueryText());

            return query;
        }

        /// <summary>
        /// Prints the provided query's full materialized query text using <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, "BaseInfo" will be used.
        /// </param>
        /// <returns></returns>
        public static ObjectQuery DebugQuery(this ObjectQuery query, [CallerFilePath] string queryName = "")
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? nameof(BaseInfo)
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
        /// Prints the provided query's full materialized query text using <see cref="LoggerExtensions.LogDebug(ILogger, string, object[])"/>
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="logger">The logger used to output the query</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, name of the generic will be used.
        /// </param>
        /// <returns></returns>
        public static ObjectQuery<TInfo> LogQuery<TInfo>(this ObjectQuery<TInfo> query, ILogger logger, [CallerFilePath] string queryName = "")
            where TInfo : BaseInfo, new()
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? typeof(TInfo).Name
                : queryName;

            logger.LogDebug("Object Query: {queryName} {queryText}", queryName, query.GetFullQueryText());

            return query;
        }

        /// <summary>
        /// Prints the provided query's full materialized query text using <see cref="LoggerExtensions.LogDebug(ILogger, string, object[])"/>
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="logger">The logger used to output the query</param>
        /// <param name="queryName">Optional Name for the query that will denote in the output where this specific query starts and ends.
        /// If no value is supplied, the filename containing the calling method will be used. If null or an empty string is supplied, "BaseInfo" will be used.
        /// </param>
        /// <returns></returns>
        public static ObjectQuery LogQuery(this ObjectQuery query, ILogger logger, [CallerFilePath] string queryName = "")
        {
            queryName = string.IsNullOrWhiteSpace(queryName)
                ? nameof(BaseInfo)
                : queryName;

            logger.LogDebug("Object Query: {queryName} {queryText}", queryName, query.GetFullQueryText());

            return query;
        }
    }
}

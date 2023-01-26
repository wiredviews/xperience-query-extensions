using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;
using CMS.Helpers;
using XperienceCommunity.QueryExtensions.DataSets;

namespace CMS.DataEngine
{
    public static class XperienceCommunityConnectionHelper
    {

        /// <summary>
        /// Executes and returns as a DataSet (or a table with no rows if none found)
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteAsync(this DocumentQuery baseQuery)
        {
            var results = await baseQuery.ExecuteReaderAsync();
            return DataReaderToDataSet(results);
        }

        /// <summary>
        /// Executes and returns as a DataSet (or a table with no rows if none found)
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteAsync<TDocument>(this DocumentQuery<TDocument> baseQuery) where TDocument : TreeNode, new()
        {
            var results = await baseQuery.ExecuteReaderAsync();
            return DataReaderToDataSet(results);
        }

        /// <summary>
        /// Executes and returns as a DataSet (or a table with no rows if none found)
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteAsync(this MultiDocumentQuery baseQuery)
        {
            var results = await baseQuery.ExecuteReaderAsync();
            return DataReaderToDataSet(results);
        }

        /// <summary>
        /// Executes the given <see cref="QueryInfo" /> asynchronously
        /// </summary>
        /// <param name="fullQueryName">The full name of a <see cref="QueryInfo" /> following the pattern CLASS_NAME.QUERY_CODE_NAME</param>
        /// <param name="parameters">Query parameters</param>
        /// <param name="queryMacros">Query macro values</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A dataset with its first table populated by executing the <see cref="QueryInfo" /> with the given parameters</returns>
        /// <execption cref="Exception">Thrown if the <see cref="QueryInfo" /> cannot be found</execption>
        public static async Task<DataSet> ExecuteQueryAsync(string fullQueryName, QueryDataParameters parameters, QueryMacros? queryMacros = null, CancellationToken token = default)
        {
            string[] querynameSplit = fullQueryName.Split('.');
            string queryClassName = "";
            string queryCodeName = "";

            if (querynameSplit.Length < 2)
            {
                throw new Exception($"Could not parse query class name and query code name from query name [{fullQueryName}]");
            }

            if (querynameSplit.Length > 2)
            {
                queryClassName = $"{querynameSplit[0]}.{querynameSplit[1]}";
                queryCodeName = string.Join(".", querynameSplit.Skip(2));
            }

            return await ExecuteQueryAsync(queryClassName, queryCodeName, parameters, queryMacros, token);
        }

        /// <summary>
        /// Executes the given <see cref="QueryInfo" /> asynchronously
        /// </summary>
        /// <param name="queryClassName">The CLASS_NAME of the <see cref="QueryInfo" /></param>
        /// <param name="queryCodeName">The CODE_NAME of the <see cref="QueryInfo" /></param>
        /// <param name="parameters">Query parameters</param>
        /// <param name="queryMacros">Query macro values</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A dataset with its first table populated by executing the <see cref="QueryInfo" /> with the given parameters</returns>
        /// <execption cref="Exception">Thrown if the <see cref="QueryInfo" /> cannot be found</execption>
        public static async Task<DataSet> ExecuteQueryAsync(string queryClassName, string queryCodeName, QueryDataParameters parameters, QueryMacros? queryMacros = null, CancellationToken token = default)
        {
            var query = await GetCachedQueryAsync(queryClassName, queryCodeName, token: token);

            if (query is null)
            {
                throw new Exception($"No query found for class name [{queryClassName}] and query name [{queryCodeName}]");
            }

            using var context = new CMSConnectionScope(query.QueryConnectionString);

            string? queryText = (queryMacros ?? new QueryMacros()).ResolveMacros(query.QueryText);

            var reader = await ConnectionHelper.ExecuteReaderAsync(queryText, parameters, query.QueryType, CommandBehavior.Default, token);
            return DbDataReaderToDataSet(reader);
        }

        /// <summary>
        /// Runs the query.
        /// </summary>
        /// <param name="queryText">Query text</param>
        /// <param name="parameters">Query parameters</param>
        /// <param name="queryType">Query type</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A dataset with query results</returns>
        public static async Task<DataSet> ExecuteQueryAsync(string queryText, QueryDataParameters parameters, QueryTypeEnum queryType, CancellationToken token = default)
        {
            var reader = await ConnectionHelper.ExecuteReaderAsync(queryText, parameters, queryType, CommandBehavior.Default, token);
            return DbDataReaderToDataSet(reader);
        }

        /// <summary>
        /// Converts a DbDataReader to a DataSet, handles multiple tables in return result.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DataSet DbDataReaderToDataSet(DbDataReader reader)
        {
            if (reader is null)
            {
                return new DataSet().AddEmptyTable();
            }

            var ds = new DataSet();
            // read each data result into a datatable
            do
            {
                var table = new DataTable();
                table.Load(reader);
                ds.Tables.Add(table);
            } while (!reader.IsClosed);

            return ds;
        }

        /// <summary>
        /// Converts a DbDataReader to a DataSet, handles multiple tables in return result.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DataSet DataReaderToDataSet(IDataReader reader)
        {
            if (reader is null)
            {
                return new DataSet().AddEmptyTable();
            }

            var ds = new DataSet();
            // read each data result into a datatable
            do
            {
                var table = new DataTable();
                table.Load(reader);
                ds.Tables.Add(table);
            } while (!reader.IsClosed);

            return ds;
        }

        /// <summary>
        /// Retrieves and caches the <see cref="QueryInfo" /> matching the provided parameters
        /// </summary>
        /// <param name="className">The CMS object CLASS_NAME of the query</param>
        /// <param name="queryCodeName">The query CODE_NAME</param>
        /// <param name="cacheLengthMinutes">An optional cache lifetime (in minutes). Defaults to 1440. Clamped between 1 and <see cref="int.MaxValue" /></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>The <see cref="QueryInfo" /> if a match is found, and null otherwise</returns>
        public static Task<QueryInfo?> GetCachedQueryAsync(string className, string queryCodeName, int cacheLengthMinutes = 1440, CancellationToken token = default) =>
            CacheHelper.CacheAsync(
                async cs =>
                {
                    var results = await new ObjectQuery<QueryInfo>()
                        .Where($"ClassID in (Select top 1 CMS_Class.ClassID from CMS_Class where ClassName = '{SqlHelper.EscapeQuotes(className)}')")
                        .WhereEquals(nameof(QueryInfo.QueryName), queryCodeName)
                        .GetEnumerableTypedResultAsync(cancellationToken: token);

                    var result = results.FirstOrDefault();

                    if (result is null)
                    {
                        cs.Cached = false;

                        return result;
                    }

                    cs.GetCacheDependency = () => CacheHelper.GetCacheDependency($"{QueryInfo.OBJECT_TYPE}|byid|{result.QueryID}");

                    return result;
                }
                , new CacheSettings(Math.Clamp(cacheLengthMinutes, 1, int.MaxValue), nameof(GetCachedQueryAsync), className, queryCodeName));
    }

}

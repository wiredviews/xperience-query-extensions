using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.Helpers;
using XperienceCommunity.QueryExtensions.Objects;

namespace CMS.DataEngine
{
    public static class XperienceCommunityConnectionHelperExtensions
    {
        /// <summary>
        /// Executes the given CMS query asynchronously
        /// </summary>
        /// <param name="fullQueryName">The full name of a CMS query following the pattern CLASS_NAME.QUERY_CODE_NAME</param>
        /// <param name="parameters"></param>
        /// <param name="queryMacros"></param>
        /// <param name="token"></param>
        /// <returns>A dataset with its first table populated by executing the CMS query with the given parameters</returns>
        /// <execption cref="Exception">Thrown if the CMS query cannot be found</execption>
        public static async Task<DataSet> ExecuteQueryAsync(string fullQueryName, QueryDataParameters parameters, QueryMacros? queryMacros = null, CancellationToken token = default)
        {
            string[] querynameSplit = fullQueryName.Split('.');
            string className = "";
            string queryCodeName = "";

            if (querynameSplit.Length < 2)
            {
                throw new Exception($"Could not parse query class name and query code name from query name [{fullQueryName}]");
            }
            
            if (querynameSplit.Length > 2)
            {
                className = $"{querynameSplit[0]}.{querynameSplit[1]}";
                queryCodeName = string.Join(".", querynameSplit.Skip(2));
            }

            return await ExecuteQueryAsync(className, queryCodeName, parameters, queryMacros, token);
        }

        /// <summary>
        /// Executes the given CMS query asynchronously
        /// </summary>
        /// <param name="className">The CLASS_NAME of the CMS query</param>
        /// <param name="queryCodeName">The CODE_NAME of the CMS query</param>
        /// <param name="parameters"></param>
        /// <param name="queryMacros"></param>
        /// <param name="token"></param>
        /// <returns>A dataset with its first table populated by executing the CMS query with the given parameters</returns>
        /// <execption cref="Exception">Thrown if the CMS query cannot be found</execption>
        public static async Task<DataSet> ExecuteQueryAsync(string className, string queryCodeName, QueryDataParameters parameters, QueryMacros? queryMacros = null, CancellationToken token = default)
        {
            var query = await GetCachedQueryAsync(className, queryCodeName);
         
            if (query is null)
            {
                throw new Exception($"No query found for class name [{className}] and query name [{queryCodeName}]");
            }

            using var context = new CMSConnectionScope(query.QueryConnectionString);

            var queryText = (queryMacros ?? new QueryMacros()).ResolveMacros(query.QueryText);

            var reader = await ConnectionHelper.ExecuteReaderAsync(queryText, parameters, query.QueryType, CommandBehavior.Default, token);
            var table = new DataTable();
            table.Load(reader);

            var ds = new DataSet();
            ds.Tables.Add(table);
            
            return ds;
        }

        public static Task<QueryInfo?> GetCachedQueryAsync(string className, string queryCodeName) =>
            CacheHelper.CacheAsync(
                cs => new ObjectQuery<QueryInfo>()
                    .Where($"ClassID in (Select top 1 CMS_Class.ClassID from CMS_Class where ClassName = '{SqlHelper.EscapeQuotes(className)}')")
                    .WhereEquals(nameof(QueryInfo.QueryName), queryCodeName)
                    .FirstOrDefaultAsync()
                , new CacheSettings(1440, nameof(GetCachedQueryAsync), className, queryCodeName));
    }
}

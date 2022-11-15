using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CMS.DataEngine;
using XperienceCommunity.QueryExtensions.DataSets;

namespace XperienceCommunity.QueryExtensions.Objects
{
    public static class XperienceCommunityObjectQueryMaterializationExtensions
    {
        /// <summary>
        /// Executes the current query and returns it as a DataSet.  Extension method to convert ExecuteReaderAsync's IDataReader into a DataSet.
        /// </summary>
        /// <typeparam name="TObject">The Object Type</typeparam>
        /// <param name="baseQuery"></param>
        /// <param name="commandBehavior">Command behavior for the reader.</param>
        /// <param name="newConnection">If true, the reader will be executed using its own dedicated connection.</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Returns a task returning either the data set with one table.</returns>
        public static async Task<DataSet> ExecuteAsync<TObject>(this ObjectQuery<TObject> baseQuery, CommandBehavior commandBehavior = CommandBehavior.Default, bool newConnection = false, CancellationToken? cancellationToken = null) where TObject : BaseInfo, new()
        {
            var reader = await baseQuery.ExecuteReaderAsync(commandBehavior, newConnection, cancellationToken);
            return DataReaderToDataSet(reader);
        }

        /// <summary>
        /// Executes the current query and returns it as a DataSet.  Extension method to convert ExecuteReaderAsync's IDataReader into a DataSet.
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <param name="commandBehavior">Command behavior for the reader.</param>
        /// <param name="newConnection">If true, the reader will be executed using its own dedicated connection.</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Returns a task returning either the data set with one table.</returns>
        public static async Task<DataSet> ExecuteAsync(this ObjectQuery baseQuery, CommandBehavior commandBehavior = CommandBehavior.Default, bool newConnection = false, CancellationToken? cancellationToken = null)
        {
            var reader = await baseQuery.ExecuteReaderAsync(commandBehavior, newConnection, cancellationToken);
            return DataReaderToDataSet(reader);
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
    }
}

using System.Data;

namespace XperienceCommunity.QueryExtensions.DataSets
{
    internal static class XperienceCommunityDataSetExtensions
    {
        /// <summary>
        /// Adds an empty <see cref="DataTable" /> to the <paramref name="ds" /> so
        /// no guards need to be added to access dataset.Tables[0].
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static DataSet AddEmptyTable(this DataSet ds)
        {
            ds.Tables.Add(new DataTable());
            
            return ds;
        }
    }
}

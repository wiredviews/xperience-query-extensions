using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CMS.DataEngine;
using Microsoft.Extensions.Logging;

namespace XperienceCommunity.QueryExtensions.Objects
{
    public static class XperienceCommunityObjectQueryMaterializationExtensions
    {
        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{TInfo}"/> of the generic Object type
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<TInfo>> ToListAsync<TInfo>(this ObjectQuery<TInfo> query, CancellationToken token = default)
            where TInfo : BaseInfo
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.ToList();
        }

        /// <summary>
        /// Converts the <paramref name="query"/> to a <see cref="List{BaseInfo}"/> of <see cref="BaseInfo" />
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<IList<BaseInfo>> ToListAsync(this ObjectQuery query, CancellationToken token = default)
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result.ToList();
        }

        /// <summary>
        /// Returns the first item of the <paramref name="query"/> as the generic Object type and null if no items were returned.
        /// /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<TInfo?> FirstOrDefaultAsync<TInfo>(this ObjectQuery<TInfo> query, CancellationToken token = default)
            where TInfo : BaseInfo
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the first item of the <paramref name="query"/> as a <see cref="BaseInfo" /> and null if no items were returned.
        /// </summary>
        /// <param name="query">The current ObjectQuery</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns></returns>
        public static async Task<BaseInfo?> FirstOrDefaultAsync(this ObjectQuery query, CancellationToken token = default)
        {
            var result = await query.GetEnumerableTypedResultAsync(cancellationToken: token);

            return result?.FirstOrDefault();
        }

    }
}

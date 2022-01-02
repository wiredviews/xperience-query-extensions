using System;
using CMS.DataEngine;

namespace XperienceCommunity.QueryExtensions.Objects
{
    public static class XperienceCommunityObjectQueryExtensions
    {
        /// <summary>
        /// Allows the caller to specify an action that has access to the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <typeparam name="TInfo"></typeparam>
        /// <returns></returns>
        public static ObjectQuery<TInfo> Tap<TInfo>(this ObjectQuery<TInfo> query, Action<ObjectQuery<TInfo>> action)
            where TInfo : BaseInfo, new()
        {
            action(query);

            return query;
        }

        /// <summary>
        /// Allows the caller to specify an action that has access to the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ObjectQuery Tap(this ObjectQuery query, Action<ObjectQuery> action)
        {
            action(query);

            return query;
        }

        /// <summary>
        /// Executes the <paramref name="ifTrueAction" /> if the <paramref name="condition" /> is true, otherwise executes
        /// the <paramref name="elseAction" /> if it is provided.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="ifTrueAction"></param>
        /// <param name="elseAction"></param>
        /// <typeparam name="TInfo"></typeparam>
        /// <returns></returns>
        public static ObjectQuery<TInfo> If<TInfo>(
            this ObjectQuery<TInfo> query, bool condition,
            Action<ObjectQuery<TInfo>> ifTrueAction,
            Action<ObjectQuery<TInfo>>? elseAction = null)
            where TInfo : BaseInfo, new()
        {
            if (condition)
            {
                ifTrueAction(query);
            }
            else if (elseAction is object)
            {
                elseAction(query);
            }

            return query;
        }

        /// <summary>
        /// Executes the <paramref name="ifTrueAction" /> if the <paramref name="condition" /> is true, otherwise executes
        /// the <paramref name="elseAction" /> if it is provided.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="ifTrueAction"></param>
        /// <param name="elseAction"></param>
        /// <returns></returns>
        public static ObjectQuery If(
            this ObjectQuery query, bool condition,
            Action<ObjectQuery> ifTrueAction,
            Action<ObjectQuery>? elseAction = null)
        {
            if (condition)
            {
                ifTrueAction(query);
            }
            else if (elseAction is object)
            {
                elseAction(query);
            }

            return query;
        }
    }
}

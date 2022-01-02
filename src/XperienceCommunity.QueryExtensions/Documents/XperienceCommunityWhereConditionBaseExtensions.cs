using System.Collections.Generic;
using System.Linq;
using CMS.DataEngine;
using CMS.DocumentEngine;

// Sourced from https://github.com/wiredviews/xperience-query-extensions/pull/1/files by https://github.com/ChristopherBass

namespace XperienceCommunity.QueryExtensions.Documents
{
    public static class XperienceCommunityWhereConditionBaseExtensions
    {
        /// <summary>
        /// Filters the data to include only documents on given path(s).
        /// </summary>
        /// <typeparam name="TQuery">Type of the data query</typeparam>
        /// <param name="condition">The query being filtered upon</param>
        /// <param name="paths">List of document paths</param>
        /// <returns>The filtered query</returns>
        /// <remarks>
        /// DocumentQuery.Path() adds parameters to a property "Paths", but if you are building a where condition that needs to 'OR' the path filter, 
        /// it won't work since DocumentQuery.Path() doesn't add the path filter into the Where logic until query execution.
        /// </remarks>
        public static TQuery WhereInPath<TQuery>(this WhereConditionBase<TQuery> condition, params string[] paths) where TQuery : WhereConditionBase<TQuery>, new()
        {
            bool combined = paths.Count() > 1;
            var customWhereCondition = new WhereCondition();

            foreach (string current in paths)
            {
                customWhereCondition.Or().Where(new IWhereCondition[]
                {
                    TreePathUtils.GetAliasPathCondition(current, false, combined)
                });
            }

            return condition.Where(customWhereCondition);
        }

        /// <summary>
        /// Filters the data to include only documents on given path.
        /// </summary>
        /// <typeparam name="TQuery">Type of the data query</typeparam>
        /// <param name="condition">The query being filtered upon</param>
        /// <param name="path">Document path</param>
        /// <param name="type">Path type to define selection scope</param>
        /// <returns>The filtered query</returns>
        /// <remarks>
        /// DocumentQuery.Path() adds parameters to a property "Paths", but if you are building a where condition that needs to 'OR' the path filter, 
        /// it won't work since DocumentQuery.Path() doesn't add the path filter into the Where logic until query execution.
        /// </remarks>
        public static TQuery WhereInPath<TQuery>(this WhereConditionBase<TQuery> condition, string path, PathTypeEnum type = PathTypeEnum.Explicit) where TQuery : WhereConditionBase<TQuery>, new()
        {
            var paths = new List<string>();

            switch (type)
            {
                case PathTypeEnum.Single:
                {
                    path = SqlHelper.EscapeLikeQueryPatterns(path, true, true, true);
                    paths.Add(path);
                    break;
                }
                case PathTypeEnum.Children:
                {
                    path = SqlHelper.EscapeLikeQueryPatterns(path, true, true, true);
                    paths.Add(TreePathUtils.EnsureChildPath(path));
                    break;
                }
                case PathTypeEnum.Section:
                {
                    path = SqlHelper.EscapeLikeQueryPatterns(path, true, true, true);
                    paths.Add(TreePathUtils.EnsureChildPath(path));
                    paths.Add(TreePathUtils.EnsureSinglePath(path));
                    break;
                }

                case PathTypeEnum.Explicit:
                    break;
                default:
                    break;
            }

            bool combined = paths.Count > 1;
            var customWhereCondition = new WhereCondition();

            foreach (string current in paths)
            {
                customWhereCondition.Or().Where(new IWhereCondition[]
                {
                    TreePathUtils.GetAliasPathCondition(current, false, combined)
                });
            }

            return condition.Where(customWhereCondition);
        }
    }
}

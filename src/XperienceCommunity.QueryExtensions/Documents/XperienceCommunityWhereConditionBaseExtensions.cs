using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.DocumentEngine;

namespace CMS.DataEngine
{
    public static class XperienceCommunityWhereConditionBaseExtensions
    {
        /// <summary>
        /// Filters the data to include only documents on given path(s).
        /// </summary>
        /// <typeparam name="TQuery">Type of the data query</typeparam>
        /// <param name="baseQuery">The query being filtered upon</param>
        /// <param name="paths">List of document paths</param>
        /// <returns>The filtered query</returns>
        /// <remarks>DocumentQuery.Path() adds parameters to a property "Paths", but if you are building a where condition that needs to 'OR' the path filter, it won't work since DocumentQuery.Path() doesn't add the path filter into the Where logic until query execution.</remarks>
        public static TQuery WhereInPath<TQuery>(this WhereConditionBase<TQuery> baseQuery, params string[] paths) where TQuery : WhereConditionBase<TQuery>, new()
        {
            var whereCondition = new WhereCondition();
            bool combined = paths.Count() > 1;
            foreach (string current in paths)
            {
                whereCondition.Or().Where(new IWhereCondition[]
                {
                    TreePathUtils.GetAliasPathCondition(current, false, combined)
                });
            }
            return baseQuery.Where(whereCondition);
        }

        /// <summary>
        /// Filters the data to include only documents on given path.
        /// </summary>
        /// <typeparam name="TQuery">Type of the data query</typeparam>
        /// <param name="baseQuery">The query being filtered upon</param>
        /// <param name="path">Document path</param>
        /// <param name="type">Path type to define selection scope</param>
        /// <returns>The filtered query</returns>
        /// <remarks>DocumentQuery.Path() adds parameters to a property "Paths", but if you are building a where condition that needs to 'OR' the path filter, it won't work since DocumentQuery.Path() doesn't add the path filter into the Where logic until query execution.</remarks>
        public static TQuery WhereInPath<TQuery>(this WhereConditionBase<TQuery> baseQuery, string path, PathTypeEnum type = PathTypeEnum.Explicit) where TQuery : WhereConditionBase<TQuery>, new()
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
            }
            var whereCondition = new WhereCondition();
            bool combined = paths.Count > 1;
            foreach (string current in paths)
            {
                whereCondition.Or().Where(new IWhereCondition[]
                {
                    TreePathUtils.GetAliasPathCondition(current, false, combined)
                });
            }
            return baseQuery.Where(whereCondition);
        }
    }
}
using CMS.DataEngine;

namespace XperienceCommunity.QueryExtensions.Objects
{
    public static class XperienceCommunityObjectQueryJoinExtensions
    {
        /// <summary>
        /// Joins the given source with another
        /// </summary>
        /// <typeparam name="TObject">The source table type being joined</typeparam>
        /// <param name="source"></param>
        /// <param name="leftColumn">The left column of the JOIN (from the <paramref name="source"/>)</param>
        /// <param name="rightColumn">The right column of the JOIN (<typeparamref name="TObject"/>)</param>
        /// <param name="tableAlias">The Alias of the <typeparamref name="TObject"/> table</param>
        /// <param name="joinType">The type of JOIN</param>
        /// <param name="additionalCondition">Additional JOIN condition, this will be added with AND operator to the base condition</param>
        /// <param name="hints">Table hints, see <see cref="SqlHints"/></param>
        /// <returns></returns>
        public static QuerySource Join<TObject>(
            this QuerySource source,
            string leftColumn, string rightColumn,
            string tableAlias,
            JoinTypeEnum joinType = JoinTypeEnum.Inner,
            IWhereCondition? additionalCondition = null,
            string[]? hints = null)
            where TObject : BaseInfo, new()
        {
            var objSource = new ObjectSource<TObject>();
            QuerySourceTable querySourceTable = objSource;
            querySourceTable.Alias = tableAlias;
            querySourceTable.Hints = hints;

            return source.Join(querySourceTable, leftColumn, rightColumn, additionalCondition, joinType);
        }

        /// <summary>
        /// Inner Joins the given source with another
        /// </summary>
        /// <typeparam name="TObject">The source table type being joined</typeparam>
        /// <param name="source"></param>
        /// <param name="leftColumn">The left column of the JOIN (from the <paramref name="source"/>)</param>
        /// <param name="rightColumn">The right column of the JOIN (<typeparamref name="TObject"/>)</param>
        /// <param name="tableAlias">The Alias of the <typeparamref name="TObject"/> table</param>
        /// <param name="additionalCondition">Additional JOIN condition, this will be added with AND operator to the base condition</param>
        /// <param name="hints">Table hints, see <see cref="SqlHints"/></param>
        /// <returns></returns>
        public static QuerySource InnerJoin<TObject>(
            this QuerySource source,
            string leftColumn, string rightColumn,
            string tableAlias,
            IWhereCondition? additionalCondition = null,
            string[]? hints = null)
            where TObject : BaseInfo, new()
        {
            var objSource = new ObjectSource<TObject>();
            QuerySourceTable querySourceTable = objSource;
            querySourceTable.Alias = tableAlias;
            querySourceTable.Hints = hints;

            return source.Join(querySourceTable, leftColumn, rightColumn, additionalCondition);
        }

        /// <summary>
        /// Left Outer Joins the given source with another
        /// </summary>
        /// <typeparam name="TObject">The source table type being joined</typeparam>
        /// <param name="source"></param>
        /// <param name="leftColumn">The left column of the JOIN (from the <paramref name="source"/>)</param>
        /// <param name="rightColumn">The right column of the JOIN (<typeparamref name="TObject"/>)</param>
        /// <param name="tableAlias">The Alias of the <typeparamref name="TObject"/> table</param>
        /// <param name="additionalCondition">Additional JOIN condition, this will be added with AND operator to the base condition</param>
        /// <param name="hints">Table hints, see <see cref="SqlHints"/></param>
        /// <returns></returns>
        public static QuerySource LeftJoin<TObject>(
            this QuerySource source,
            string leftColumn, string rightColumn,
            string tableAlias,
            IWhereCondition? additionalCondition = null,
            string[]? hints = null)
            where TObject : BaseInfo, new()
        {
            var objSource = new ObjectSource<TObject>();
            QuerySourceTable querySourceTable = objSource;
            querySourceTable.Alias = tableAlias;
            querySourceTable.Hints = hints;

            return source.Join(querySourceTable, leftColumn, rightColumn, additionalCondition, JoinTypeEnum.LeftOuter);
        }

        /// <summary>
        /// Right Outer Joins the given source with another
        /// </summary>
        /// <typeparam name="TObject">The source table type being joined</typeparam>
        /// <param name="source"></param>
        /// <param name="leftColumn">The left column of the JOIN (from the <paramref name="source"/>)</param>
        /// <param name="rightColumn">The right column of the JOIN (<typeparamref name="TObject"/>)</param>
        /// <param name="tableAlias">The Alias of the <typeparamref name="TObject"/> table</param>
        /// <param name="additionalCondition">Additional JOIN condition, this will be added with AND operator to the base condition</param>
        /// <param name="hints">Table hints, see <see cref="SqlHints"/></param>
        /// <returns></returns>
        public static QuerySource RightJoin<TObject>(
            this QuerySource source,
            string leftColumn, string rightColumn,
            string tableAlias,
            IWhereCondition? additionalCondition = null,
            string[]? hints = null)
            where TObject : BaseInfo, new()
        {
            var objSource = new ObjectSource<TObject>();
            QuerySourceTable querySourceTable = objSource;
            querySourceTable.Alias = tableAlias;
            querySourceTable.Hints = hints;

            return source.Join(querySourceTable, leftColumn, rightColumn, additionalCondition, JoinTypeEnum.RightOuter);
        }
    }
}

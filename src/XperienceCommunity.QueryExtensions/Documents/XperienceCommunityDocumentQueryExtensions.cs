using System;
using CMS.DocumentEngine;

namespace XperienceCommunity.QueryExtensions.Documents
{
    public static class XperienceCommunityDocumentQueryExtensions
    {
        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeGUID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeGuid">Value of the <see cref="TreeNode.NodeGUID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereNodeGUIDEquals<TNode>(this DocumentQuery<TNode> query, Guid nodeGuid) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.NodeGUID), nodeGuid);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeGUID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeGuid">Value of the <see cref="TreeNode.NodeGUID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery WhereNodeGUIDEquals(this DocumentQuery query, Guid nodeGuid) =>
            query.WhereEquals(nameof(TreeNode.NodeGUID), nodeGuid);

        /// <summary>
        /// Returns the <see cref="MultiDocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeGUID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="nodeGuid">Value of the <see cref="TreeNode.NodeGUID" /> to filter by</param>
        /// <returns></returns>
        public static MultiDocumentQuery WhereNodeGUIDEquals(this MultiDocumentQuery query, Guid nodeGuid) =>
            query.WhereEquals(nameof(TreeNode.NodeGUID), nodeGuid);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeID">Value of the <see cref="TreeNode.NodeID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereNodeIDEquals<TNode>(this DocumentQuery<TNode> query, int nodeID) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.NodeID), nodeID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="nodeID">Value of the <see cref="TreeNode.NodeID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery WhereNodeIDEquals(this DocumentQuery query, int nodeID) =>
            query.WhereEquals(nameof(TreeNode.NodeID), nodeID);

        /// <summary>
        /// Returns the <see cref="MultiDocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.NodeID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="nodeID">Value of the <see cref="TreeNode.NodeID" /> to filter by</param>
        /// <returns></returns>
        public static MultiDocumentQuery WhereNodeIDEquals(this MultiDocumentQuery query, int nodeID) =>
            query.WhereEquals(nameof(TreeNode.NodeID), nodeID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.DocumentID"/> matching the provided value
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="documentID">Value of the <see cref="TreeNode.DocumentID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> WhereDocumentIDEquals<TNode>(this DocumentQuery<TNode> query, int documentID) where TNode : TreeNode, new() =>
            query.WhereEquals(nameof(TreeNode.DocumentID), documentID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.DocumentID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <param name="documentID">Value of the <see cref="TreeNode.DocumentID" /> to filter by</param>
        /// <returns></returns>
        public static DocumentQuery WhereDocumentIDEquals(this DocumentQuery query, int documentID) =>
            query.WhereEquals(nameof(TreeNode.DocumentID), documentID);

        /// <summary>
        /// Returns the <see cref="MultiDocumentQuery"/> filtered to a single Node with a <see cref="TreeNode.DocumentID"/> matching the provided value
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <param name="documentID">Value of the <see cref="TreeNode.DocumentID" /> to filter by</param>
        /// <returns></returns>
        public static MultiDocumentQuery WhereDocumentIDEquals(this MultiDocumentQuery query, int documentID) =>
            query.WhereEquals(nameof(TreeNode.DocumentID), documentID);

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> ordered by <see cref="TreeNode.NodeOrder"/>
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="query">The current DocumentQuery</param>
        /// <returns></returns>
        public static DocumentQuery<TNode> OrderByNodeOrder<TNode>(this DocumentQuery<TNode> query) where TNode : TreeNode, new() =>
            query.OrderBy(nameof(TreeNode.NodeOrder));

        /// <summary>
        /// Returns the <see cref="DocumentQuery"/> ordered by <see cref="TreeNode.NodeOrder"/>
        /// </summary>
        /// <param name="query">The current DocumentQuery</param>
        /// <returns></returns>
        public static DocumentQuery OrderByNodeOrder(this DocumentQuery query) =>
            query.OrderBy(nameof(TreeNode.NodeOrder));

        /// <summary>
        /// Returns the <see cref="MultiDocumentQuery"/> ordered by <see cref="TreeNode.NodeOrder"/>
        /// </summary>
        /// <param name="query">The current MultiDocumentQuery</param>
        /// <returns></returns>
        public static MultiDocumentQuery OrderByNodeOrder(this MultiDocumentQuery query) =>
            query.OrderBy(nameof(TreeNode.NodeOrder));

        /// <summary>
        /// Allows the caller to specify an action that has access to the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <typeparam name="TNode"></typeparam>
        /// <returns></returns>
        public static DocumentQuery<TNode> Tap<TNode>(this DocumentQuery<TNode> query, Action<DocumentQuery<TNode>> action)
            where TNode : TreeNode, new()
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
        public static DocumentQuery Tap(this DocumentQuery query, Action<DocumentQuery> action)
        {
            action(query);

            return query;
        }

        /// <summary>
        /// Allow the caller to specify an action that has access to the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static MultiDocumentQuery Tap(this MultiDocumentQuery query, Action<MultiDocumentQuery> action)
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
        /// <typeparam name="TNode"></typeparam>
        /// <returns></returns>
        public static DocumentQuery<TNode> If<TNode>(
            this DocumentQuery<TNode> query, bool condition,
            Action<DocumentQuery<TNode>> ifTrueAction,
            Action<DocumentQuery<TNode>>? elseAction = null)
            where TNode : TreeNode, new()
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
        public static DocumentQuery If(
            this DocumentQuery query, bool condition,
            Action<DocumentQuery> ifTrueAction,
            Action<DocumentQuery>? elseAction = null)
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
        public static MultiDocumentQuery If(
            this MultiDocumentQuery query, bool condition,
            Action<MultiDocumentQuery> ifTrueAction,
            Action<MultiDocumentQuery>? elseAction = null)
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

using System;
using System.Linq;
using AutoFixture;
using CMS.DocumentEngine;
using CMS.Tests;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using XperienceCommunity.QueryExtensions.Documents;

namespace XperienceCommunity.QueryExtensions.Tests.Documents
{
    public class XperienceCommunityDocumentQueryExtensionsTests : UnitTests
    {
        [Test]
        public void DocumentQuery_WhereNodeGUIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            var nodeGUID = fixture.Create<Guid>();

            var sut = new DocumentQuery<TreeNode>();

            var result = sut.WhereNodeGUIDEquals(nodeGUID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@NodeGUID");
            param.Value.Should().Be(nodeGUID);
            result.WhereCondition.Should().Be($"[NodeGUID] = @NodeGUID");
        }

        [Test]
        public void MultiDocumentQuery_WhereNodeGUIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            var nodeGUID = fixture.Create<Guid>();

            var sut = new MultiDocumentQuery();

            var result = sut.WhereNodeGUIDEquals(nodeGUID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@NodeGUID");
            param.Value.Should().Be(nodeGUID);
            result.WhereCondition.Should().Be($"[NodeGUID] = @NodeGUID");
        }

        [Test]
        public void DocumentQuery_WhereNodeIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            int nodeID = fixture.Create<int>();

            var sut = new DocumentQuery<TreeNode>();

            var result = sut.WhereNodeIDEquals(nodeID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@NodeID");
            param.Value.Should().Be(nodeID);
            result.WhereCondition.Should().Be($"[NodeID] = @NodeID");
        }

        [Test]
        public void MultiDocumentQuery_WhereNodeIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            int nodeID = fixture.Create<int>();

            var sut = new MultiDocumentQuery();

            var result = sut.WhereNodeIDEquals(nodeID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@NodeID");
            param.Value.Should().Be(nodeID);
            result.WhereCondition.Should().Be($"[NodeID] = @NodeID");
        }

        [Test]
        public void DocumentQuery_WhereDocumentIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            int documentID = fixture.Create<int>();

            var sut = new DocumentQuery<TreeNode>();

            var result = sut.WhereDocumentIDEquals(documentID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@DocumentID");
            param.Value.Should().Be(documentID);
            result.WhereCondition.Should().Be($"[DocumentID] = @DocumentID");
        }

        [Test]
        public void MultiDocumentQuery_WhereDocumentIDEquals_Will_Add_A_New_Where_Condition()
        {
            var fixture = new Fixture();
            int documentID = fixture.Create<int>();

            var sut = new MultiDocumentQuery();

            var result = sut.WhereDocumentIDEquals(documentID);

            var param = result.Parameters.Single();
            param.Name.Should().Be("@DocumentID");
            param.Value.Should().Be(documentID);
            result.WhereCondition.Should().Be($"[DocumentID] = @DocumentID");
        }

        [Test]
        public void DocumentQuery_OrderByNodeOrder_Will_Add_A_New_OrderBy_Condition()
        {
            var sut = new DocumentQuery<TreeNode>();

            var result = sut.OrderByNodeOrder();

            result.OrderByColumns.Should().Be($"NodeOrder");
        }

        [Test]
        public void MultiDocumentQuery_DocumentQuery_OrderByNodeOrder_Will_Add_A_New_OrderBy_Condition()
        {
            var sut = new MultiDocumentQuery();

            var result = sut.OrderByNodeOrder();

            result.OrderByColumns.Should().Be($"NodeOrder");
        }

        [Test]
        public void DocumentQuery_Tap_Will_Execute_The_Given_Action()
        {
            var sut = new DocumentQuery<TreeNode>();

            var action = Substitute.For<Action<DocumentQuery<TreeNode>>>();

            var result = sut.Tap(action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void MultiDocumentQuery_Tap_Will_Execute_The_Given_Action()
        {
            var sut = new MultiDocumentQuery();

            var action = Substitute.For<Action<MultiDocumentQuery>>();

            var result = sut.Tap(action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void DocumentQuery_If_Will_Execute_The_Given_Action_If_The_Condition_Is_True()
        {
            var sut = new DocumentQuery<TreeNode>();

            var action = Substitute.For<Action<DocumentQuery<TreeNode>>>();

            var result = sut.If(true, action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void MultiDocumentQuery_If_Will_Execute_The_Given_Action_If_The_Condition_Is_True()
        {
            var sut = new MultiDocumentQuery();

            var action = Substitute.For<Action<MultiDocumentQuery>>();

            var result = sut.If(true, action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void DocumentQuery_If_Will_Execute_The_ElseAction_If_The_Condition_Is_False()
        {
            var sut = new DocumentQuery<TreeNode>();

            var action = Substitute.For<Action<DocumentQuery<TreeNode>>>();
            var elseAction = Substitute.For<Action<DocumentQuery<TreeNode>>>();

            var result = sut.If(false, action, elseAction);

            action.ReceivedCalls().Should().BeEmpty();
            elseAction.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void MultiDocumentQuery_If_Will_Execute_The_ElseAction_If_The_Condition_Is_False()
        {
            var sut = new MultiDocumentQuery();

            var action = Substitute.For<Action<MultiDocumentQuery>>();
            var elseAction = Substitute.For<Action<MultiDocumentQuery>>();

            var result = sut.If(false, action, elseAction);

            action.ReceivedCalls().Should().BeEmpty();
            elseAction.ReceivedCalls().Should().HaveCount(1);
        }
    }
}

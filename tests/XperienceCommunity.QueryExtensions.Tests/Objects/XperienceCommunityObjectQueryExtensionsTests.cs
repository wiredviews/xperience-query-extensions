using System;
using CMS.DataEngine;
using CMS.EventLog;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using XperienceCommunity.QueryExtensions.Objects;

namespace XperienceCommunity.QueryExtensions.Tests.Objects
{
    public class XperienceCommunityObjectQueryExtensionsTests
    {
        [Test]
        public void ObjectQueryT_Tap_Will_Execute_The_Given_Action()
        {
            var sut = new ObjectQuery<EventLogInfo>();

            var action = Substitute.For<Action<ObjectQuery<EventLogInfo>>>();

            var result = sut.Tap(action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void ObjectQuery_Tap_Will_Execute_The_Given_Action()
        {
            var sut = new ObjectQuery();

            var action = Substitute.For<Action<ObjectQuery>>();

            var result = sut.Tap(action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void ObjectQueryT_If_Will_Execute_The_Given_Action_If_The_Condition_Is_True()
        {
            var sut = new ObjectQuery<EventLogInfo>();

            var action = Substitute.For<Action<ObjectQuery<EventLogInfo>>>();

            var result = sut.If(true, action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void ObjectQuery_If_Will_Execute_The_Given_Action_If_The_Condition_Is_True()
        {
            var sut = new ObjectQuery();

            var action = Substitute.For<Action<ObjectQuery>>();

            var result = sut.If(true, action);

            action.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void ObjectQueryT_If_Will_Execute_The_ElseAction_If_The_Condition_Is_False()
        {
            var sut = new ObjectQuery<EventLogInfo>();

            var action = Substitute.For<Action<ObjectQuery<EventLogInfo>>>();
            var elseAction = Substitute.For<Action<ObjectQuery<EventLogInfo>>>();

            var result = sut.If(false, action, elseAction);

            action.ReceivedCalls().Should().BeEmpty();
            elseAction.ReceivedCalls().Should().HaveCount(1);
        }

        [Test]
        public void ObjectQuery_If_Will_Execute_The_ElseAction_If_The_Condition_Is_False()
        {
            var sut = new ObjectQuery();

            var action = Substitute.For<Action<ObjectQuery>>();
            var elseAction = Substitute.For<Action<ObjectQuery>>();

            var result = sut.If(false, action, elseAction);

            action.ReceivedCalls().Should().BeEmpty();
            elseAction.ReceivedCalls().Should().HaveCount(1);
        }
    }
}

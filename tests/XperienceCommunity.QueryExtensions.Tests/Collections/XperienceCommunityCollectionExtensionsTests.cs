using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using XperienceCommunity.QueryExtensions.Collections;

namespace XperienceCommunity.QueryExtensions.Tests.Collections
{
    public class XperienceCommunityCollectionExtensionsTests
    {

        [Test]
        public void Tap_Does_Not_Execute_The_Action_With_An_Empty_Sequence()
        {
            var source = Enumerable.Empty<int>();

            var action = Substitute.For<Action<int>>();

            var result = source.Tap(action);

            action.ReceivedCalls().Should().BeEmpty();
        }

        [Test]
        public void Tap_Executes_The_Action_For_Each_Item_In_The_Sequence()
        {
            int[] source = new int[] { 1, 2, 3 };

            var action = Substitute.For<Action<int>>();

            var result = source.Tap(action);

            action.ReceivedCalls().Should().HaveCount(3);
        }

        [Test]
        public async Task TapAsync_Does_Not_Execute_The_Action_With_An_Empty_Sequence()
        {
            var source = Task.FromResult(Enumerable.Empty<int>());

            var action = Substitute.For<Action<int>>();

            var result = await source.TapAsync(action);

            action.ReceivedCalls().Should().BeEmpty();
        }

        [Test]
        public async Task TapAsync_Executes_The_Action_For_Each_Item_In_The_Sequence()
        {
            var source = Task.FromResult(new int[] { 1, 2, 3 }.AsEnumerable());

            var action = Substitute.For<Action<int>>();

            var result = await source.TapAsync(action);

            action.ReceivedCalls().Should().HaveCount(3);
        }

        [Test]
        public async Task SelectAsync_Does_Not_Execute_The_Action_With_An_Empty_Sequence()
        {
            var source = Task.FromResult(Enumerable.Empty<int>());

            var projection = Substitute.For<Func<int, string>>();
            projection(Arg.Do<int>(i => i.ToString()));

            var result = await source.SelectAsync(projection);

            result.Should().BeEquivalentTo(Array.Empty<int>());
            projection.ReceivedCalls().Should().BeEmpty();
        }

        [Test]
        public async Task SelectAsync_Executes_The_Action_For_Each_Item_In_The_Sequence()
        {
            var source = Task.FromResult(new int[] { 1, 2, 3 }.AsEnumerable());

            var projection = Substitute.For<Func<int, string>>();
            projection(Arg.Any<int>()).Returns((c) => c[0].ToString());

            var result = await source.SelectAsync(projection);


            result.Should().BeEquivalentTo(new[] { "1", "2", "3" });
            projection.ReceivedCalls().Should().HaveCount(3);
        }
    }
}

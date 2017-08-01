using System;
using System.Reactive.Linq;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using Xunit;
using static Microsoft.Reactive.Testing.ReactiveTest;

namespace Rx.Test
{
    public class RxTest
    {
        [Fact]
        public void ThrottleTest()
        {
            var scheduler = new TestScheduler();

            var observable = scheduler
                .CreateHotObservable(
                    OnNext(TimeSpan.FromSeconds(1).Ticks, "s"),
                    OnNext(TimeSpan.FromSeconds(2).Ticks, "se"),
                    OnNext(TimeSpan.FromSeconds(3).Ticks, "sea"),
                    OnNext(TimeSpan.FromSeconds(4).Ticks + 1, "sear"),
                    OnNext(TimeSpan.FromSeconds(5).Ticks, "searc"),
                    OnNext(TimeSpan.FromSeconds(6).Ticks, "search"))
                .Throttle(TimeSpan.FromSeconds(1), scheduler);

            var observer = scheduler
                .Start(
                    () => observable,
                    created: 0,
                    subscribed: 0,
                    disposed: TimeSpan.FromSeconds(10).Ticks);

            observer
                .Messages
                .Should()
                .Equal(
                    OnNext(TimeSpan.FromSeconds(4).Ticks, "sea"),
                    OnNext(TimeSpan.FromSeconds(7).Ticks, "search"));
        }
    }
}

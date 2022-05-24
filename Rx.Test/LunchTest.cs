using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Reactive.Testing;
using System.Collections.Immutable;
using System.Reactive;
using System.Reactive.Linq;
using Xunit;
using static Microsoft.Reactive.Testing.ReactiveTest;
using static Rx.Test.ImmutableDictionaryExtensions;

namespace Rx.Test
{
    public class LunchTest
    {
        [Fact]
        public void LunchVotingShouldWork()
        {
            var scheduler = new TestScheduler();
            var date = DateTime.Today.ToDateTimeOffset();
            var deadline = date.Add(new TimeSpan(12, 00, 00));
            var observer = scheduler.Start(
                () => scheduler
                    .CreateHotObservable(
                        OnNext(date.Add(06, 00, 00).Ticks, "Asia"),
                        OnNext(date.Add(11, 27, 37).Ticks, "Asia"),
                        OnNext(date.Add(11, 35, 43).Ticks, "Billa"),
                        OnNext(date.Add(11, 47, 18).Ticks, "Asia"),
                        OnNext(date.Add(11, 53, 22).Ticks, "Fiori"),
                        OnNext(date.Add(12, 00, 00).Ticks, "Asia"),
                        OnNext(date.Add(12, 00, 01).Ticks, "Asia"))
                    .Scan(
                        ImmutableDictionary<string, int>.Empty,
                        (dict, key) => dict.AddOrUpdate(key, 1, i => i + 1))
                    .TakeUntil(deadline, scheduler),
                created: date.Ticks,
                subscribed: date.Ticks,
                disposed: date.AddDays(1).Ticks);

            var expected = new[]
            {
                OnNextDict(date.Add(06, 00, 00).Ticks, Dict("Asia", 1)),
                OnNextDict(date.Add(11, 27, 37).Ticks, Dict("Asia", 2)),
                OnNextDict(date.Add(11, 35, 43).Ticks, Dict("Asia", 2).Add("Billa", 1)),
                OnNextDict(date.Add(11, 47, 18).Ticks, Dict("Asia", 3).Add("Billa", 1)),
                OnNextDict(date.Add(11, 53, 22).Ticks, Dict("Asia", 3).Add("Billa", 1).Add("Fiori", 1)),
                OnNextDict(date.Add(12, 00, 00).Ticks, Dict("Asia", 4).Add("Billa", 1).Add("Fiori", 1)),
                OnCompleted(date.Add(12, 00, 00).Ticks, Dict("", 0))
            };

            observer
                .Messages
                .Should()
                .Equal(expected, (a, b) => b.Equals(a));
        }

        private static Recorded<Notification<ImmutableDictionary<TKey, TValue>>> OnNextDict<TKey, TValue>(
            long ticks,
            ImmutableDictionary<TKey, TValue> value) where TKey : notnull
        {
            return OnNext(
                ticks,
                (ImmutableDictionary<TKey, TValue> dict) => dict.StructuralEquals(value));
        }
    }
}

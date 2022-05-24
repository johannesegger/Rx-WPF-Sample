using System.Collections.Immutable;

namespace Rx.Test
{
    public static class ImmutableDictionaryExtensions
    {
        public static ImmutableDictionary<TKey, TValue> Dict<TKey, TValue>(TKey key, TValue value) where TKey : notnull
        {
            return ImmutableDictionary<TKey, TValue>.Empty.Add(key, value);
        }

        public static ImmutableDictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> o,
            TKey key, TValue add, Func<TValue, TValue> update) where TKey : notnull
        {
            if (o.TryGetValue(key, out var value))
            {
                return o.SetItem(key, update(value));
            }
            return o.Add(key, add);
        }

        public static bool StructuralEquals<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> o1,
            ImmutableDictionary<TKey, TValue> o2) where TKey : notnull
        {
            return new HashSet<KeyValuePair<TKey, TValue>>(o1)
                .SetEquals(new HashSet<KeyValuePair<TKey, TValue>>(o2));
        }
    }
}
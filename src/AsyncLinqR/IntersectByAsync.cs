﻿namespace AsyncLinqR;

public static partial class AsyncLinq
{
    // TODO Faire le produit cartésien avec IAsyncEnumerable => IEnumerable et Func de TKey ou Task<TKey>

    public static IAsyncEnumerable<TSource> IntersectByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TKey> second, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return IntersectByAsync(first, second, keySelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TSource> IntersectByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var firstSet = new HashSet<TKey>(comparer);
        var secondSet = new HashSet<TKey>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            secondSet.Add(item);

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var key = keySelector(item);
            if (secondSet.Contains(key) && firstSet.Add(key))
                yield return item;
        }
    }
}
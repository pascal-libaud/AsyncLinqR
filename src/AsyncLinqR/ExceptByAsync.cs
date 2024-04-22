namespace AsyncLinqR;

public static partial class AsyncLinq
{
    // TODO Faire le produit cartésien avec IAsyncEnumerable => IEnumerable et Func de TKey ou Task<TKey>

    public static IAsyncEnumerable<TSource> ExceptByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TKey> second, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return first.ExceptByAsync(second, keySelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TSource> ExceptByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<TKey>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken))
            set.Add(item);

        await foreach (var item in first.WithCancellation(cancellationToken))
            if (set.Add(keySelector(item)))
                yield return item;
    }
}
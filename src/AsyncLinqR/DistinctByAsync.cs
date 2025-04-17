namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var hash = new HashSet<TKey>();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (hash.Add(await selector(item).ConfigureAwait(false)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            if (hash.Add(await selector(item).ConfigureAwait(false)))
                yield return item;
        }
    }
}
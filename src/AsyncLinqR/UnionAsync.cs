namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<T> UnionAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.UnionAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> UnionAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;

        // Can also be done this way
        //return first.ConcatAsync(second, cancellationToken).WhereAsync(x => set.Add(x), cancellationToken);
    }

    public static IAsyncEnumerable<T> UnionAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.UnionAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> UnionAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in first)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (set.Add(item))
                yield return item;
        }

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;
    }

    public static IAsyncEnumerable<T> UnionAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.UnionAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> UnionAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in second)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (set.Add(item))
                yield return item;
        }
    }
}
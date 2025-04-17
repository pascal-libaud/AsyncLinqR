namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<T> ExceptAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.ExceptAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> ExceptAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            set.Add(item);

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;
    }

    public static IAsyncEnumerable<T> ExceptAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.ExceptAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> ExceptAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            set.Add(item);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in first)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (set.Add(item))
                yield return item;
        }
    }

    public static IAsyncEnumerable<T> ExceptAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return first.ExceptAsync(second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> ExceptAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in second)
        {
            cancellationToken.ThrowIfCancellationRequested();
            set.Add(item);
        }

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (set.Add(item))
                yield return item;
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<T> IntersectAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return IntersectAsync(first, second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> IntersectAsync<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var firstSet = new HashSet<T>(comparer);
        var secondSet = new HashSet<T>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            secondSet.Add(item);

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (secondSet.Contains(item) && firstSet.Add(item))
                yield return item;
    }

    public static IAsyncEnumerable<T> IntersectAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return IntersectAsync(first, second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> IntersectAsync<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var firstSet = new HashSet<T>(comparer);
        var secondSet = new HashSet<T>(comparer);

        await foreach (var item in second.WithCancellation(cancellationToken).ConfigureAwait(false))
            secondSet.Add(item);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in first)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (secondSet.Contains(item) && firstSet.Add(item))
                yield return item;
        }
    }

    public static IAsyncEnumerable<T> IntersectAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, CancellationToken cancellationToken = default)
    {
        return IntersectAsync(first, second, null, cancellationToken);
    }

    public static async IAsyncEnumerable<T> IntersectAsync<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var firstSet = new HashSet<T>(comparer);
        var secondSet = new HashSet<T>(comparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in second)
        {
            cancellationToken.ThrowIfCancellationRequested();
            secondSet.Add(item);
        }

        await foreach (var item in first.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (secondSet.Contains(item) && firstSet.Add(item))
                yield return item;
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>();
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            set.Add(item);

        return set;
    }

    public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> source, IEqualityComparer<T> comparer, CancellationToken cancellationToken = default)
    {
        var set = new HashSet<T>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            set.Add(item);

        return set;
    }
}
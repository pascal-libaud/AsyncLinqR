namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);
        
        if (await enumerator.MoveNextAsync().ConfigureAwait(false))
            return enumerator.Current;

        throw new InvalidOperationException("Sequence contains no elements");
    }

    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> FirstAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item).ConfigureAwait(false))
                return item;
        }

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(item).ConfigureAwait(false))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);
        if (await enumerator.MoveNextAsync())
            return enumerator.Current;

        throw new InvalidOperationException("Sequence contains no elements");
    }

    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
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
            if (await predicate(item))
                return item;
        }

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (await predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }
}
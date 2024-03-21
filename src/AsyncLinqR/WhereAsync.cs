namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> WhereAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (predicate(item))
                yield return item;

    }

    public static async IAsyncEnumerable<T> WhereAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> WhereAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (await predicate(item))
                yield return item;
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> PrependAsync<T>(this IAsyncEnumerable<T> source, T item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        yield return item;

        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return t;
    }

    public static async IAsyncEnumerable<T> PrependAsync<T>(this IEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        yield return await item.ConfigureAwait(false);

        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return t;
        }
    }

    public static async IAsyncEnumerable<T> PrependAsync<T>(this IAsyncEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        yield return await item.ConfigureAwait(false);

        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return t;
    }
}
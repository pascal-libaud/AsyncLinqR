namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> AppendAsync<T>(this IAsyncEnumerable<T> source, T item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return t;

        yield return item;
    }

    public static async IAsyncEnumerable<T> AppendAsync<T>(this IEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return t;
        }

        yield return await item.ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<T> AppendAsync<T>(this IAsyncEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return t;

        yield return await item.ConfigureAwait(false);
    }
}
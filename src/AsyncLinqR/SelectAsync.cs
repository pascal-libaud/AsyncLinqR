namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return await selector(item).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return selector(item);
    }

    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await selector(item).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return await selector(item, index++).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            yield return selector(item, index++);
    }

    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int index = 0;
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await selector(item, index++).ConfigureAwait(false);
        }
    }
}
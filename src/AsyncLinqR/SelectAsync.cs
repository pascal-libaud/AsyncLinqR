namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, Task<U>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            yield return await selector(item);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, U> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            yield return selector(item);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await selector(item);
        }
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, int, Task<U>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken))
            yield return await selector(item, index++);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, int, U> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken))
            yield return selector(item, index++);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IEnumerable<T> source, Func<T, int, Task<U>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int index = 0;
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await selector(item, index++);
        }
    }
}
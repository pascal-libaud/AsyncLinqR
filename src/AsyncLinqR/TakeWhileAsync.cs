namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (predicate(item))
                yield return item;
            else
                break;
    }

    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item).ConfigureAwait(false))
                yield return item;
            else
                break;
        }
    }

    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(item).ConfigureAwait(false))
                yield return item;
            else
                break;
    }

    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, int,bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (predicate(item, index++))
                yield return item;
            else
                break;
    }

    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IEnumerable<T> source, Func<T, int,Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item, index++).ConfigureAwait(false))
                yield return item;
            else
                break;
        }
    }

    public static async IAsyncEnumerable<T> TakeWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, int,Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(item, index++).ConfigureAwait(false))
                yield return item;
            else
                break;
    }
}
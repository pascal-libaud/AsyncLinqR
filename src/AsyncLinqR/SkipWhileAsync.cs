namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        bool isRespected = true;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            isRespected = isRespected && predicate(item);
            if (!isRespected)
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        bool isRespected = true;
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            isRespected = isRespected && await predicate(item).ConfigureAwait(false);
            if (!isRespected)
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        bool isRespected = true;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            isRespected = isRespected && await predicate(item).ConfigureAwait(false);
            if (!isRespected)
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, int,bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        bool isRespected = true;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            index++;
            isRespected = isRespected && predicate(item, index);
            if (!isRespected)
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IEnumerable<T> source, Func<T, int,Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int index = 0;
        bool isRespected = true;
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            index++;
            isRespected = isRespected && await predicate(item, index).ConfigureAwait(false);
            if (!isRespected)
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> SkipWhileAsync<T>(this IAsyncEnumerable<T> source, Func<T, int,Task<bool>> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int index = 0;
        bool isRespected = true;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            index++;
            isRespected = isRespected && await predicate(item, index).ConfigureAwait(false);
            if (!isRespected)
                yield return item;
        }
    }
}
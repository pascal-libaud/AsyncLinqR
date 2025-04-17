namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var _ in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result++;

        return result;
    }

    public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(t).ConfigureAwait(false))
                result++;

        return result;
    }

    public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var t in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (predicate(t))
                result++;

        return result;
    }

    public static async Task<int> CountAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(t).ConfigureAwait(false))
                result++;
        }

        return result;
    }
}
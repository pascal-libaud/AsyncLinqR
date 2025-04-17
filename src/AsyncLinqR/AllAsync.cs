namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<bool> AllAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (!await predicate(item).ConfigureAwait(false))
                return false;

        return true;
    }

    public static async Task<bool> AllAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (!predicate(item))
                return false;

        return true;
    }

    public static async Task<bool> AllAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!await predicate(item).ConfigureAwait(false))
                return false;
        }

        return true;
    }
}
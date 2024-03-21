namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<bool> AllAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (!await predicate(item))
                return false;

        return true;
    }

    public static async Task<bool> AllAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (!predicate(item))
                return false;

        return true;
    }

    public static async Task<bool> AllAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!await predicate(item))
                return false;
        }

        return true;
    }
}
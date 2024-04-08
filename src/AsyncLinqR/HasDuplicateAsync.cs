namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<bool> HasDuplicateAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<T>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(item))
                return true;
        }

        return false;
    }

    public static async Task<bool> HasDuplicateAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(selector(item)))
                return true;
        }

        return false;
    }

    public static async Task<bool> HasDuplicateAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<TKey>();
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!hash.Add(await selector(item)))
                return true;
        }

        return false;
    }

    public static async Task<bool> HasDuplicateAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(await selector(item)))
                return true;
        }

        return false;
    }
}
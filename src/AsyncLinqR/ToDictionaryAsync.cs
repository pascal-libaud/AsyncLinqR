namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source, CancellationToken cancellationToken = default) where TKey : notnull
    {
        return source.ToDictionaryAsync(null, cancellationToken);
    }

    public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default) where TKey : notnull
    {
        var result = new Dictionary<TKey, TValue>(comparer);
        await foreach (var (key, value) in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(key, value);

        return result;
    }

    // ----------------------------------- //

    public static Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<(TKey Key, TValue Value)> source, CancellationToken cancellationToken = default) where TKey : notnull
    {
        return source.ToDictionaryAsync(null, cancellationToken);
    }

    public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<(TKey Key, TValue Value)> source, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default) where TKey : notnull
    {
        var result = new Dictionary<TKey, TValue>(comparer);
        await foreach (var (key, value) in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(key, value);

        return result;
    }

    // ----------------------------------- //

    public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default) where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull
    {
        return source.ToDictionaryAsync<TSource, TKey>(keySelector, null, cancellationToken);
    }

    public static async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default) where TKey : notnull
    {
        var result = new Dictionary<TKey, TSource>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(keySelector(item), item);

        return result;
    }

    public static async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default) where TKey : notnull
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = new Dictionary<TKey, TSource>(comparer);
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(await keySelector(item).ConfigureAwait(false), item);
        }

        return result;
    }

    public static async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default) where TKey : notnull
    {
        var result = new Dictionary<TKey, TSource>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(await keySelector(item).ConfigureAwait(false), item); 

        return result;
    }

    // ----------------------------------- //

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync<TSource, TKey, TElement>(keySelector, elementSelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, Task<TElement>> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, Task<TElement>> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, Task<TElement>> elementSelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return source.ToDictionaryAsync<TSource, TKey, TElement>(keySelector, elementSelector, null, cancellationToken);
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, TElement>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(keySelector(item), elementSelector(item));

        return result;
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = new Dictionary<TKey, TElement>(comparer);
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(await keySelector(item).ConfigureAwait(false), elementSelector(item));
        }

        return result;
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, TElement>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(await keySelector(item).ConfigureAwait(false), elementSelector(item));

        return result;
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, Task<TElement>> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, TElement>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(keySelector(item), await elementSelector(item).ConfigureAwait(false));

        return result;
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, Task<TElement>> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = new Dictionary<TKey, TElement>(comparer);
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(await keySelector(item).ConfigureAwait(false), await elementSelector(item).ConfigureAwait(false));
        }

        return result;
    }

    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> keySelector, Func<TSource, Task<TElement>> elementSelector, IEqualityComparer<TKey>? comparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, TElement>(comparer);
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            result.Add(await keySelector(item).ConfigureAwait(false), await elementSelector(item).ConfigureAwait(false));

        return result;
    }
}
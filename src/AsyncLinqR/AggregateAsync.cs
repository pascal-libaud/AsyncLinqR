namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> AggregateAsync<T>(this IAsyncEnumerable<T> source, Func<T, T, T> func, CancellationToken cancellationToken = default)
    {
        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);
        
        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
            throw new InvalidOperationException("The sequence contains no element");

        T current = enumerator.Current;

        while (await enumerator.MoveNextAsync().ConfigureAwait(false))
            current = func(current, enumerator.Current);

        return current;
    }

    public static async Task<T> AggregateAsync<T>(this IEnumerable<T> source, Func<T, T, Task<T>> func, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("The sequence contains no element");

        T current = enumerator.Current;

        while (enumerator.MoveNext())
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = await func(current, enumerator.Current).ConfigureAwait(false);
        }

        return current;
    }

    public static async Task<T> AggregateAsync<T>(this IAsyncEnumerable<T> source, Func<T, T, Task<T>> func, CancellationToken cancellationToken = default)
    {
        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);

        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
            throw new InvalidOperationException("The sequence contains no element");

        T current = enumerator.Current;

        while (await enumerator.MoveNextAsync().ConfigureAwait(false))
            current = await func(current, enumerator.Current).ConfigureAwait(false);

        return current;
    }

    public static async Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = func(current, item);

        return current;
    }

    public static async Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var current = seed;

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = await func(current, item).ConfigureAwait(false);
        }

        return current;
    }

    public static async Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = await func(current, item).ConfigureAwait(false);

        return current;
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = func(current, item);

        return resultSelector(current);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var current = seed;

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = await func(current, item).ConfigureAwait(false);
        }

        return resultSelector(current);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = await func(current, item).ConfigureAwait(false);

        return resultSelector(current);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var current = seed;

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = func(current, item);
        }

        return await resultSelector(current).ConfigureAwait(false);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = func(current, item);

        return await resultSelector(current).ConfigureAwait(false);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, Func<TAccumulate, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var current = seed;

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = await func(current, item).ConfigureAwait(false);
        }

        return await resultSelector(current).ConfigureAwait(false);
    }

    public static async Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, Task<TAccumulate>> func, Func<TAccumulate, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        var current = seed;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            current = await func(current, item).ConfigureAwait(false);

        return await resultSelector(current).ConfigureAwait(false);
    }
}
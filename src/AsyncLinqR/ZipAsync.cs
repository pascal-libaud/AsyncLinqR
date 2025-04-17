namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<(TFirst First, TSecond Second)> ZipAsync<TFirst, TSecond>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, CancellationToken cancellationToken = default)
    {
        return first.ZipAsync(second, (x, y) => (x, y), cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (await enumerator1.MoveNextAsync().ConfigureAwait(false) && await enumerator2.MoveNextAsync().ConfigureAwait(false))
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (enumerator1.MoveNext() && await enumerator2.MoveNextAsync().ConfigureAwait(false))
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        using var enumerator2 = second.GetEnumerator();

        while (await enumerator1.MoveNextAsync().ConfigureAwait(false) && enumerator2.MoveNext())
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (await enumerator1.MoveNextAsync().ConfigureAwait(false) && await enumerator2.MoveNextAsync().ConfigureAwait(false))
            yield return await resultSelector(enumerator1.Current, enumerator2.Current).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (enumerator1.MoveNext() && await enumerator2.MoveNextAsync().ConfigureAwait(false))
            yield return await resultSelector(enumerator1.Current, enumerator2.Current).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        using var enumerator2 = second.GetEnumerator();

        while (await enumerator1.MoveNextAsync().ConfigureAwait(false) && enumerator2.MoveNext())
            yield return await resultSelector(enumerator1.Current, enumerator2.Current).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        using var enumerator2 = second.GetEnumerator();

        cancellationToken.ThrowIfCancellationRequested();
        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(enumerator1.Current, enumerator2.Current).ConfigureAwait(false);
        }
    }
}
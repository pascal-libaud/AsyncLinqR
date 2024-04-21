namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<(TFirst First, TSecond Second)> ZipAsync<TFirst, TSecond>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, CancellationToken cancellationToken = default)
    {
        return first.ZipAsync(second, (x, y) => (x, y), cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (await enumerator1.MoveNextAsync() && await enumerator2.MoveNextAsync())
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (enumerator1.MoveNext() && await enumerator2.MoveNextAsync())
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        using var enumerator2 = second.GetEnumerator();

        while (await enumerator1.MoveNextAsync() && enumerator2.MoveNext())
            yield return resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (await enumerator1.MoveNextAsync() && await enumerator2.MoveNextAsync())
            yield return await resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (enumerator1.MoveNext() && await enumerator2.MoveNextAsync())
            yield return await resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        using var enumerator2 = second.GetEnumerator();

        while (await enumerator1.MoveNextAsync() && enumerator2.MoveNext())
            yield return await resultSelector(enumerator1.Current, enumerator2.Current);
    }

    public static async IAsyncEnumerable<TResult> ZipAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        using var enumerator2 = second.GetEnumerator();

        cancellationToken.ThrowIfCancellationRequested();
        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(enumerator1.Current, enumerator2.Current);
        }
    }
}
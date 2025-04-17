namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<(TFirst? First, TSecond? Second)> ZipFullAsync<TFirst, TSecond>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, CancellationToken cancellationToken = default)
    {
        return first.ZipFullAsync(second, (x, y) => (x, y), cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync().ConfigureAwait(false);
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync().ConfigureAwait(false);
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (true)
        {
            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync().ConfigureAwait(false);
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        using var enumerator2 = second.GetEnumerator();

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync().ConfigureAwait(false);
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = enumerator2.MoveNext();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync().ConfigureAwait(false);
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync().ConfigureAwait(false);
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2).ConfigureAwait(false);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        var enumerator2 = second.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator2 = enumerator2.ConfigureAwait(false);

        while (true)
        {
            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync().ConfigureAwait(false);
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2).ConfigureAwait(false);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator1 = enumerator1.ConfigureAwait(false);
        using var enumerator2 = second.GetEnumerator();

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync().ConfigureAwait(false);
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = enumerator2.MoveNext();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2).ConfigureAwait(false);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        using var enumerator2 = second.GetEnumerator();

        cancellationToken.ThrowIfCancellationRequested();
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = enumerator2.MoveNext();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2).ConfigureAwait(false);
            else
                break;
        }
    }
}
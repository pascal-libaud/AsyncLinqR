namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<(TFirst? First, TSecond? Second)> ZipFullAsync<TFirst, TSecond>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, CancellationToken cancellationToken = default)
    {
        return first.ZipFullAsync(second, (x, y) => (x, y), cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync();
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
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (true)
        {
            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        using var enumerator2 = second.GetEnumerator();

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync();
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
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var enumerator1 = first.GetEnumerator();
        await using var enumerator2 = second.GetAsyncEnumerator(cancellationToken);

        while (true)
        {
            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = await enumerator2.MoveNextAsync();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2);
            else
                break;
        }
    }

    public static async IAsyncEnumerable<TResult> ZipFullAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var enumerator1 = first.GetAsyncEnumerator(cancellationToken);
        using var enumerator2 = second.GetEnumerator();

        while (true)
        {
            bool hasValue1 = await enumerator1.MoveNextAsync();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = enumerator2.MoveNext();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return await resultSelector(value1, value2);
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
                yield return await resultSelector(value1, value2);
            else
                break;
        }
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T?> DefaultIfEmptyAsync<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);

        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
        {
            yield return default;
            yield break;
        }

        do
        {
            yield return enumerator.Current;
        } while (await enumerator.MoveNextAsync().ConfigureAwait(false));
    }

    public static async IAsyncEnumerable<T> DefaultIfEmptyAsync<T>(this IAsyncEnumerable<T> source, T defaultValue, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);

        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
        {
            yield return defaultValue;
            yield break;
        }

        do
        {
            yield return enumerator.Current;
        } while (await enumerator.MoveNextAsync().ConfigureAwait(false));
    }
}
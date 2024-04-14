namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T?> DefaultIfEmptyAsync<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);

        if (!await enumerator.MoveNextAsync())
        {
            yield return default;
            yield break;
        }

        do
        {
            yield return enumerator.Current;
        } while (await enumerator.MoveNextAsync());
    }

    public static async IAsyncEnumerable<T> DefaultIfEmptyAsync<T>(this IAsyncEnumerable<T> source, T defaultValue, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);

        if (!await enumerator.MoveNextAsync())
        {
            yield return defaultValue;
            yield break;
        }

        do
        {
            yield return enumerator.Current;
        } while (await enumerator.MoveNextAsync());
    }
}
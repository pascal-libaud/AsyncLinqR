namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> ConcatAsync<T>(this IAsyncEnumerable<T> source1, IAsyncEnumerable<T> source2, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source1.WithCancellation(cancellationToken))
            yield return item;

        await foreach (var item in source2.WithCancellation(cancellationToken))
            yield return item;
    }
}
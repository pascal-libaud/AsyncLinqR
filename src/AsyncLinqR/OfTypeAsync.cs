namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TResult> OfTypeAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (item is TResult result)
                yield return result;
    }
}
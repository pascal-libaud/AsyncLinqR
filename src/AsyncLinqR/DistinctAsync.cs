namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> DistinctAsync<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<T>();

        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (hash.Add(item))
                yield return item;
        }
    }
}
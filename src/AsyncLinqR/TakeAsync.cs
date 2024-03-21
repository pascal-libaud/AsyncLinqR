namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> TakeAsync<T>(this IAsyncEnumerable<T> source, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (count > 0)
        {
            await foreach (var item in source.WithCancellation(cancellationToken))
            {
                yield return item;
                if (--count <= 0)
                    break;
            }
        }
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }
    }
}
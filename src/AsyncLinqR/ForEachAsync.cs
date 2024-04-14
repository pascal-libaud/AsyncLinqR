namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> source, Action<T> action, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            action(item);
    }
}
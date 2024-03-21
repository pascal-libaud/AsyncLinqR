namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<bool> IsEmptyAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await foreach (var _ in source.WithCancellation(cancellationToken))
            return false;

        return true;
    }
}
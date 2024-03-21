namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return (await source.ToListAsync(cancellationToken)).ToArray();
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<(int Index, T Item)> IndexAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return source.SelectAsync((item, index) => (index, item), cancellationToken);
    }
}
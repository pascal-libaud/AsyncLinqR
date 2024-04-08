namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncOrderedEnumerable<T> OrderAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<T, T>(source, x => x, null, true, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<T> OrderAsync<T>(this IAsyncEnumerable<T> source, IComparer<T> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<T, T>(source, x => x, null, true, cancellationToken, comparer);
    }

    public static IAsyncOrderedEnumerable<T> OrderDescendingAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<T, T>(source, x => x, null, false, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<T> OrderDescendingAsync<T>(this IAsyncEnumerable<T> source, IComparer<T> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<T, T>(source, x => x, null, false, cancellationToken, comparer);
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<int> InfiniteIteratorAsync([EnumeratorCancellation] CancellationToken cancellation = default)
    {
        int increment = 0;
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return increment++;
        }
    }

    public static async IAsyncEnumerable<int> InfiniteIteratorAsync(int start, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return start++;
        }
    }

#if NET7_0_OR_GREATER

    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>([EnumeratorCancellation] CancellationToken cancellation = default) where T : INumber<T>
    {
        T increment = T.Zero;
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return increment++;
        }
    }

    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>(T start, [EnumeratorCancellation] CancellationToken cancellation = default) where T : INumber<T>
    {
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return start++;
        }
    }

#endif
}
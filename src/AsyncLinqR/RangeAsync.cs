namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<int> RangeAsync(int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (int i = 0; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<int> RangeAsync(int start, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (int i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<int?> RangeNullableAsync(int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (int i = 0; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<int?> RangeNullableAsync(int start, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (int i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

#if NET7_0_OR_GREATER

    public static async IAsyncEnumerable<T> RangeAsync<T>(T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = T.Zero; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T> RangeAsync<T>(T start, T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = T.Zero; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T start, T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : struct, INumber<T>
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

#endif
}
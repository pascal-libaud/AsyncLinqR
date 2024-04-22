namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> RepeatAsync<T>(T element, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        while (count-- > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return element;
        }
    }
}
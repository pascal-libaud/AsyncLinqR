namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> EmptyAsync<T>()
    {
        await Task.Yield();
        yield break;
    }
}
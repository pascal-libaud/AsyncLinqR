namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, Task<U>> selector)
    {
        await foreach (var item in source)
            yield return await selector(item);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, U> selector)
    {
        await foreach (var item in source)
            yield return selector(item);
    }

    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> selector)
    {
        foreach (var item in source)
            yield return await selector(item);
    }
}
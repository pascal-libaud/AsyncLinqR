namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> DistinctByAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, U> selector)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source)
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> DistinctByAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> selector)
    {
        var hash = new HashSet<U>();
        foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> DistinctByAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, Task<U>> selector)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }
}
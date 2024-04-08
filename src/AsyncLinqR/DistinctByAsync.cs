namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source)
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, Task<TKey>> selector)
    {
        var hash = new HashSet<TKey>();
        foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<TSource> DistinctByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<TKey>> selector)
    {
        var hash = new HashSet<TKey>();
        await foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }
}
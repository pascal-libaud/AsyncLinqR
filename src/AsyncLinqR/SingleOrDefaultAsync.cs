namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (await enumerator.MoveNextAsync())
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        return default;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source)
            if (predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        bool found = false;
        T candidate = default!;
        foreach (var item in source)
            if (await predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source)
            if (await predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }
}
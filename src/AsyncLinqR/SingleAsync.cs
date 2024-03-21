namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (await enumerator.MoveNextAsync())
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        throw new InvalidOperationException("Sequence contains no elements");
    }

    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    public static async Task<T> SingleAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }
}
namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);
        if (await enumerator.MoveNextAsync())
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        return default;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source.WithCancellation(cancellationToken))
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

    public static async Task<T?> SingleOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;
        
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");
        }

        if (!found)
            return default;

        return candidate!;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source.WithCancellation(cancellationToken))
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
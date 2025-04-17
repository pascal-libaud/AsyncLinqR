namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);
        
        if (await enumerator.MoveNextAsync().ConfigureAwait(false))
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync().ConfigureAwait(false))
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        throw new InvalidOperationException("Sequence contains no elements");
    }

    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
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

    public static async Task<T> SingleAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item).ConfigureAwait(false))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");
        }

        if (!found)
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(item).ConfigureAwait(false))
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
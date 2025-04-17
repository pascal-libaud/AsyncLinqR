namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async Task<T> LastAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var enumerator = source.GetAsyncEnumerator(cancellationToken);
        await using var disposableEnumerator = enumerator.ConfigureAwait(false);

        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
            throw new InvalidOperationException("Sequence contains no elements");

        while (await enumerator.MoveNextAsync().ConfigureAwait(false)) { }

        return enumerator.Current;
    }

    public static async Task<T> LastAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        bool isFound = false;
        T candidate = default!;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (predicate(item))
            {
                isFound = true;
                candidate = item;
            }

        if (isFound)
            return candidate!;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> LastAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool isFound = false;
        T candidate = default!;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item).ConfigureAwait(false))
            {
                isFound = true;
                candidate = item;
            }
        }

        if (isFound)
            return candidate!;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> LastAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool isFound = false;
        T candidate = default!;

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            if (await predicate(item).ConfigureAwait(false))
            {
                isFound = true;
                candidate = item;
            }

        if (isFound)
            return candidate!;

        throw new InvalidOperationException("Sequence contains no matching element");
    }
}
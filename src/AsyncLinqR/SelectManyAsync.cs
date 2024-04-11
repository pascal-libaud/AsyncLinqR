namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            foreach (var result in selector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return result;
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TResult>>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in await selector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return result;
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<IEnumerable<TResult>>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            foreach (var result in await selector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return result;
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in selector(item).WithCancellation(cancellationToken))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            await foreach (var result in selector(item).WithCancellation(cancellationToken))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in await collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in await collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken))
                yield return await resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken))
                yield return await resultSelector(item, result);
    }

}
﻿namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
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
            foreach (var result in await selector(item).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return result;
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult[]>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await selector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return result;
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<List<TResult>>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await selector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return result;
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<IEnumerable<TResult>>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            foreach (var result in await selector(item).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return result;
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<TResult[]>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await selector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return result;
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<List<TResult>>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await selector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return result;
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in selector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            await foreach (var result in selector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return resultSelector(item, result);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<TCollection[]>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return resultSelector(item, result);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<List<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return resultSelector(item, result);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in await collectionSelector(item).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return resultSelector(item, result);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<TCollection[]>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return resultSelector(item, result);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<List<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return resultSelector(item, result);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result).ConfigureAwait(false);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            foreach (var result in collectionSelector(item))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result).ConfigureAwait(false);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(item, result).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<TCollection[]>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(item, result).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<List<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(item, result).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            foreach (var result in await collectionSelector(item).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item, result).ConfigureAwait(false);
            }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<TCollection[]>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(item, result).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<List<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        foreach (var result in await collectionSelector(item).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await resultSelector(item, result).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return await resultSelector(item, result).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> SelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            await foreach (var result in collectionSelector(item).WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return await resultSelector(item, result).ConfigureAwait(false);
    }

}
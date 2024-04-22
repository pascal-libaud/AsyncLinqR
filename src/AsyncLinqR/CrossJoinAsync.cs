using System.Linq;

namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = async () => secondList ??= await second.ToListAsync(cancellationToken);

        await foreach (var item1 in first.WithCancellation(cancellationToken))
            foreach (var item2 in await func())
                yield return resultSelector(item1, item2);
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = async () => secondList ??= await second.ToListAsync(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item1 in first)
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var item2 in await func())
                yield return resultSelector(item1, item2);
        }
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = () => secondList ??= second.ToList();

        await foreach (var item1 in first.WithCancellation(cancellationToken))
            foreach (var item2 in func())
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return resultSelector(item1, item2);
            }
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = async () => secondList ??= await second.ToListAsync(cancellationToken);

        await foreach (var item1 in first.WithCancellation(cancellationToken))
            foreach (var item2 in await func())
                yield return await resultSelector(item1, item2);
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = async () => secondList ??= await second.ToListAsync(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item1 in first)
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var item2 in await func())
                yield return await resultSelector(item1, item2);
        }
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = () => secondList ??= second.ToList();

        await foreach (var item1 in first.WithCancellation(cancellationToken))
            foreach (var item2 in func())
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item1, item2);
            }
    }

    public static async IAsyncEnumerable<TResult> CrossJoinAsync<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, Task<TResult>> resultSelector, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TSecond>? secondList = null;
        var func = () => secondList ??= second.ToList();

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item1 in first)
            foreach (var item2 in func())
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await resultSelector(item1, item2);
            }
    }
}
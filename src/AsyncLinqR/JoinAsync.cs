using System.Linq;

namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync<TOuter, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    // ----

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = () => innerList ??= inner.ToList();
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var o in outer)
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }

    public static IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, CancellationToken cancellationToken = default)
    {
        return outer.JoinAsync<TOuter, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector, resultSelector, null, cancellationToken);
    }

    public static async IAsyncEnumerable<TResult> JoinAsync<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, Task<TKey>> outerKeySelector, Func<TInner, Task<TKey>> innerKeySelector, Func<TOuter, TInner, Task<TResult>> resultSelector, IEqualityComparer<TKey>? comparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<TInner>? innerList = null;
        var func = async () => innerList ??= await inner.ToListAsync(cancellationToken);
        comparer ??= EqualityComparer<TKey>.Default;

        await foreach (var o in outer.WithCancellation(cancellationToken))
        foreach (var i in await func())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (comparer.Equals(await outerKeySelector(o), await innerKeySelector(i)))
                yield return await resultSelector(o, i);
        }
    }
}
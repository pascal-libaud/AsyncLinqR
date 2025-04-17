using System.Collections;
using System.Linq;

namespace AsyncLinqR;

public interface IGroup<out TKey, out TValue> : IEnumerable<TValue>
{
    TKey Key { get; }
}

public interface IAsyncGroup<out TKey, out TValue> : IAsyncEnumerable<TValue>
{
    TKey Key { get; }
}

public static partial class AsyncLinq
{
    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, null, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, null, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, (IEqualityComparer<TKey>?)null, cancellationToken);
    }

    // ----------------------------------- //

    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey>? equalityComparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, x => x, equalityComparer, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, IEqualityComparer<TKey>? equalityComparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, x => x, equalityComparer, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TValue>> GroupByAsync<TValue, TKey>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, IEqualityComparer<TKey>? equalityComparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, x => x, equalityComparer, cancellationToken);
    }

    // ----------------------------------- //

    public static IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, null, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, TResult> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, null, cancellationToken);
    }

    public static IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, TResult> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, (IEqualityComparer<TKey>?)null, cancellationToken);
    }

    public static IEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, Task<TResult>> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, null, cancellationToken);
    }

    public static IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, Task<TResult>> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, null, cancellationToken);
    }

    public static IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, Task<TResult>> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, (IEqualityComparer<TKey>?)null, cancellationToken);
    }

    public static IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, Task<TResult>> select, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return GroupByAsync(source, keySelector, select, (IEqualityComparer<TKey>?)null, cancellationToken);
    }

    // ----------------------------------- //

    public static async IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var key = keySelector(item);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new Group<TKey, TResult>(k.Key, k.Value.Select(v => select(v)))))
            yield return kv;
    }

    public static async IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, TResult> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var key = await keySelector(item).ConfigureAwait(false);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new Group<TKey, TResult>(k.Key, k.Value.Select(v => select(v)))))
            yield return kv;
    }

    public static async IAsyncEnumerable<IGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, TResult> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var key = await keySelector(item).ConfigureAwait(false);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new Group<TKey, TResult>(k.Key, k.Value.Select(v => select(v)))))
            yield return kv;
    }

    public static IEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, Task<TResult>> select, IEqualityComparer<TKey>? equalityComparer, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var key = keySelector(item);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new AsyncGroup<TKey, TResult>(k.Key, k.Value.SelectAsync(async v => await select(v).ConfigureAwait(false), cancellationToken))))
            yield return kv;
    }

    public static async IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, Task<TResult>> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var key = keySelector(item);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new AsyncGroup<TKey, TResult>(k.Key, k.Value.SelectAsync(async v => await select(v).ConfigureAwait(false), cancellationToken))))
            yield return kv;
    }

    public static async IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, Task<TResult>> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var key = await keySelector(item).ConfigureAwait(false);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new AsyncGroup<TKey, TResult>(k.Key, k.Value.SelectAsync(async v => await select(v).ConfigureAwait(false), cancellationToken))))
            yield return kv;
    }

    public static async IAsyncEnumerable<IAsyncGroup<TKey, TResult>> GroupByAsync<TValue, TKey, TResult>(this IAsyncEnumerable<TValue> source, Func<TValue, Task<TKey>> keySelector, Func<TValue, Task<TResult>> select, IEqualityComparer<TKey>? equalityComparer, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var key = await keySelector(item).ConfigureAwait(false);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.Select(k => new AsyncGroup<TKey, TResult>(k.Key, k.Value.SelectAsync(async v => await select(v).ConfigureAwait(false), cancellationToken))))
            yield return kv;
    }
}

internal class Group<TKey, TValue> : IGroup<TKey, TValue>
{
    private readonly IEnumerable<TValue> _values;

    public Group(TKey key, IEnumerable<TValue> values)
    {
        _values = values;
        Key = key;
    }

    public TKey Key { get; }

    public IEnumerator<TValue> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class AsyncGroup<TKey, TValue> : IAsyncGroup<TKey, TValue>
{
    private readonly IAsyncEnumerable<TValue> _values;

    public AsyncGroup(TKey key, IAsyncEnumerable<TValue> values)
    {
        _values = values;
        Key = key;
    }

    public TKey Key { get; }

    public IAsyncEnumerator<TValue> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return _values.GetAsyncEnumerator(cancellationToken);
    }
}
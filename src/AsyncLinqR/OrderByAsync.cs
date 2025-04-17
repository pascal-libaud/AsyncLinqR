namespace AsyncLinqR;

public interface IAsyncOrderedEnumerable<out T> : IAsyncEnumerable<T> { }

public static partial class AsyncLinq
{
    public static IAsyncOrderedEnumerable<TSource> OrderByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, null, true, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<TSource> OrderByAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, null, true, cancellationToken, comparer);
    }

    public static IAsyncOrderedEnumerable<TSource> OrderByDescendingAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, null, false, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<TSource> OrderByDescendingAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, null, false, cancellationToken, comparer);
    }

    public static IAsyncOrderedEnumerable<TSource> ThenByAsync<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, source as AsyncOrderedEnumerable<TSource>, true, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<TSource> ThenByAsync<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, source as AsyncOrderedEnumerable<TSource>, true, cancellationToken, comparer);
    }

    public static IAsyncOrderedEnumerable<TSource> ThenByDescendingAsync<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, source as AsyncOrderedEnumerable<TSource>, false, cancellationToken);
    }

    public static IAsyncOrderedEnumerable<TSource> ThenByDescendingAsync<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerable<TSource, TKey>(source, keySelector, source as AsyncOrderedEnumerable<TSource>, false, cancellationToken, comparer);
    }
}

internal abstract class AsyncOrderedEnumerable<T> : IAsyncOrderedEnumerable<T>
{
    internal abstract int Compare(T x, T y);

    public abstract IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
}

internal class AsyncOrderedEnumerable<TSource, TKey> : AsyncOrderedEnumerable<TSource>
{
    private readonly IAsyncEnumerable<TSource> _source;
    private readonly Func<TSource, TKey> _keySelector;
    private readonly AsyncOrderedEnumerable<TSource>? _parent;
    private readonly bool _isAscending;
    private readonly CancellationToken _cancellationToken;
    private readonly IComparer<TKey> _comparer;

    public AsyncOrderedEnumerable(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, AsyncOrderedEnumerable<TSource>? parent, bool isAscending, CancellationToken cancellationToken, IComparer<TKey>? comparer = null)
    {
        _source = source;
        _keySelector = keySelector;
        _parent = parent;
        _isAscending = isAscending;
        _cancellationToken = cancellationToken;
        _comparer = comparer ?? Comparer<TKey>.Default;
    }

    internal override int Compare(TSource x, TSource y)
    {
        int result = 0;
        if (_parent != null)
            result = _parent.Compare(x, y);

        if (result == 0)
            result = _isAscending ? _comparer.Compare(_keySelector(x), _keySelector(y)) : _comparer.Compare(_keySelector(y), _keySelector(x));

        return result;
    }

    public override IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new AsyncOrderedEnumerator<TSource>(this, _source, cancellationToken == default ? _cancellationToken : cancellationToken);
    }
}

internal class AsyncOrderedEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly AsyncOrderedEnumerable<T> _enumerable;
    private readonly IAsyncEnumerable<T> _source;
    private readonly CancellationToken _cancellationToken;
    private List<T>? _orderedList;
    private IEnumerator<T>? _enumerator;

    public AsyncOrderedEnumerator(AsyncOrderedEnumerable<T> enumerable, IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        _enumerable = enumerable;
        _source = source;
        _cancellationToken = cancellationToken;
    }

    public ValueTask DisposeAsync()
    {
        _enumerator?.Dispose();
        return ValueTask.CompletedTask;
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_orderedList == null)
        {
            _orderedList = await _source.ToListAsync(_cancellationToken).ConfigureAwait(false);
            _orderedList.Sort(_enumerable.Compare);
            _enumerator = _orderedList.GetEnumerator();
        }

        _cancellationToken.ThrowIfCancellationRequested();
        return _enumerator!.MoveNext();
    }

    public T Current => _enumerator!.Current;
}
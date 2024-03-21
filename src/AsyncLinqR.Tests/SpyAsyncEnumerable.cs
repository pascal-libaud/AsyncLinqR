namespace AsyncLinqR.Tests;

public static class SpyAsyncEnumerable
{
    public static SpyAsyncEnumerable<int> GetValuesAsync()
    {
        return GetValuesAsync(AsyncLinq.RangeAsync(10));
    }

    public static SpyAsyncEnumerable<int> GetValuesAsync(IAsyncEnumerable<int> source)
    {
        return new SpyAsyncEnumerable<int>(source);
    }
}

public class SpyAsyncEnumerable<T> : IAsyncEnumerable<T>, ISpyAsyncEnumerable<T>
{
    private readonly IAsyncEnumerable<T> _enumerable;

    public SpyAsyncEnumerable(IAsyncEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
    }

    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; set; } = false;
    public int CountEnumeration { get; set; } = 0;
    public int CountItemEnumerated { get; set; } = 0;

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new SpyAsyncEnumerator<T>(this, _enumerable.GetAsyncEnumerator(cancellationToken));
    }
}

file class SpyAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private bool _firstCallMade = false;
    private readonly ISpy _spied;
    private readonly IAsyncEnumerator<T> _enumerator;

    public SpyAsyncEnumerator(ISpy spied, IAsyncEnumerator<T> enumerator)
    {
        _spied = spied;
        _enumerator = enumerator;
    }

    public ValueTask DisposeAsync()
    {
        return _enumerator.DisposeAsync();
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (!_firstCallMade)
        {
            _spied.CountEnumeration++;
            _firstCallMade = true;
        }

        var result = await _enumerator.MoveNextAsync();

        if (result)
            _spied.CountItemEnumerated++;
        else
            _spied.IsEndReached = true;

        return result;
    }

    public T Current => _enumerator.Current;
}

public class SpyAsyncEnumerableTest
{
    [Fact]
    public async Task SpyEnumerable_should_work_as_expected()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        Assert.False(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        await foreach (var i in spy)
            if (i == 5)
                break;

        Assert.True(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(1, spy.CountEnumeration);
        Assert.Equal(6, spy.CountItemEnumerated);

        await foreach (var _ in spy)
        { }

        Assert.True(spy.IsEnumerated);
        Assert.True(spy.IsEndReached);
        Assert.Equal(2, spy.CountEnumeration);
        Assert.Equal(16, spy.CountItemEnumerated);
    }
}
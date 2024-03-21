using System.Collections;

namespace AsyncLinqR.Tests;

public static class SpyEnumerable
{
    public static SpyEnumerable<int> GetValues()
    {
        return new SpyEnumerable<int>(RangeHelper.Range(10));
    }
}

public class SpyEnumerable<T> : IEnumerable<T>, ISpyEnumerable<T>
{
    private readonly IEnumerable<T> _enumerable;

    public SpyEnumerable(IEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
    }

    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; set; } = false;
    public int CountEnumeration { get; set; } = 0;
    public int CountItemEnumerated { get; set; } = 0;

    public IEnumerator<T> GetEnumerator()
    {
        return new SpyEnumerator<T>(this, _enumerable.GetEnumerator());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file class SpyEnumerator<T> : IEnumerator<T>
{
    private bool _firstCallMade = false;
    private readonly ISpy _spied;
    private readonly IEnumerator<T> _enumerator;

    public SpyEnumerator(ISpy spied, IEnumerator<T> enumerator)
    {
        _spied = spied;
        _enumerator = enumerator;
    }

    public bool MoveNext()
    {
        if (!_firstCallMade)
        {
            _spied.CountEnumeration++;
            _firstCallMade = true;
        }

        var result = _enumerator.MoveNext();

        if (result)
            _spied.CountItemEnumerated++;
        else
            _spied.IsEndReached = true;

        return result;
    }

    public void Reset()
    {
        _firstCallMade = false;
        _enumerator.Reset();
    }

    public T Current => _enumerator.Current;

    object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}

public class SpyEnumerableTest
{
    [Fact]
    public void SpyEnumerable_should_work_as_expected()
    {
        var spy = SpyEnumerable.GetValues();

        Assert.False(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        foreach (var i in spy)
            if (i == 5)
                break;

        Assert.True(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(1, spy.CountEnumeration);
        Assert.Equal(6, spy.CountItemEnumerated);

        foreach (var _ in spy)
        { }

        Assert.True(spy.IsEnumerated);
        Assert.True(spy.IsEndReached);
        Assert.Equal(2, spy.CountEnumeration);
        Assert.Equal(16, spy.CountItemEnumerated);
    }
}
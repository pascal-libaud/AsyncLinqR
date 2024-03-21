namespace AsyncLinqR.Tests;

public static class TestHelper
{
    public static async Task Should_not_enumerate_early<T>(this Func<IAsyncEnumerable<int>, IAsyncEnumerable<T>> func)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        var source = func(spy);
        Assert.False(spy.IsEnumerated);

        _ = await source.TakeAsync(5).ToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    public static async Task Should_enumerate_each_item_once<T>(this Func<IAsyncEnumerable<int>, Task<T>> func, ISpyAsyncEnumerable<int>? spy = null)
    {
        spy ??= SpyAsyncEnumerable.GetValuesAsync();

        _ = await func(spy);
        Assert.Equal(1, spy.CountEnumeration);
    }

    public static async Task Should_not_enumerate_all_when<T>(this Func<IAsyncEnumerable<int>, Task<T>> func)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await func(spy);
        Assert.False(spy.IsEndReached);
    }
}
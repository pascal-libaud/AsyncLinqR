namespace AsyncLinqR.Tests;

public static class TestHelper
{
    public static async Task Should_not_enumerate_early<T>(this Func<IAsyncEnumerable<int>, IAsyncEnumerable<T>> sut)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        var source = sut(spy);
        Assert.False(spy.IsEnumerated);

        _ = await source.TakeAsync(5).ToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    public static async Task Should_enumerate_each_item_once<T>(this Func<IAsyncEnumerable<int>, Task<T>> sut,
        ISpyAsyncEnumerable<int>? spy = null)
    {
        spy ??= SpyAsyncEnumerable.GetValuesAsync();

        _ = await sut(spy);
        Assert.Equal(1, spy.CountEnumeration);
    }

    public static async Task Should_not_enumerate_all_when<T>(this Func<IAsyncEnumerable<int>, Task<T>> sut)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await sut(spy);
        Assert.False(spy.IsEndReached);
    }

    public static async Task Enumerate_all_when<T>(this Func<IAsyncEnumerable<int>, Task<T>> sut)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await sut(spy);
        Assert.True(spy.IsEndReached);
    }
}
namespace AsyncLinqR.Tests;

public class ToListAsyncTest
{
    [Fact]
    public async Task ToListAsync_should_throw_on_null_enumerable()
    {
        var func = async () =>
        {
            IAsyncEnumerable<int>? enumerable = null;
            return await enumerable.ToListAsync();
        };

        await Assert.ThrowsAsync<NullReferenceException>(func);
    }

    [Fact]
    public async Task ToList_should_enumerate_each_item()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.ToListAsync();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task ToListAsync_should_enumerate_each_item_once()
    {
        var omToListAsync = async (IAsyncEnumerable<int> x) => await x.ToListAsync();
        await omToListAsync.Should_enumerate_each_item_once();
    }
}
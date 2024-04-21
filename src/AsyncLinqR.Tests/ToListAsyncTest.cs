#pragma warning disable CS8604 // Possible null reference argument.
namespace AsyncLinqR.Tests;

public class ToListAsyncTest
{
    [Fact]
    public async Task ToListAsync_should_throw_on_null_enumerable()
    {
        var sut = async () =>
        {
            IAsyncEnumerable<int>? enumerable = null;
            return await enumerable.ToListAsync();
        };

        await Assert.ThrowsAsync<NullReferenceException>(sut);
    }

    [Fact]
    public async Task ToListAsync_should_enumerate_each_item()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.ToListAsync();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task ToListAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }
}
#pragma warning disable CS8604
namespace AsyncLinqR.Tests;

public class ToHashSetAsyncTest
{
    [Fact]
    public async Task ToHashSetAsync_should_work_as_expected()
    {
        var set = await new[] { 1, 2, 3, 2, 1 }.ToAsyncEnumerable().ToHashSetAsync();
        set.Should().BeEquivalentTo(new[] { 1, 2, 3 });
    }

    [Fact]
    public async Task ToHashSetAsync_should_throw_on_null_enumerable()
    {
        var sut = async () =>
        {
            IAsyncEnumerable<int>? enumerable = null;
            return await enumerable.ToHashSetAsync();
        };

        await Assert.ThrowsAsync<NullReferenceException>(sut);
    }

    [Fact]
    public async Task ToHashSet_should_enumerate_each_item()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.ToHashSetAsync();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task ToHashSetAsync_should_enumerate_each_item_once()
    {
        var sut = async (IAsyncEnumerable<int> x) => await x.ToHashSetAsync();
        await sut.Should_enumerate_each_item_once();
    }
}
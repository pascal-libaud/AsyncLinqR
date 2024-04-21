namespace AsyncLinqR.Tests;

public class ToDictionaryAsyncTest
{
    [Fact]
    public async Task ToDictionaryAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(0, 5).SelectAsync(x => new DummyIndexValue(x, x * 2)).ToDictionaryAsync(x => x.Index, x => x.Value);
        
        var expected = new Dictionary<int, int> { [0] = 0, [1] = 2, [2] = 4, [3] = 6, [4] = 8 };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ToDictionaryAsync_should_throw_an_exception_when_key_is_duplicated()
    {
        var sut = () => AsyncLinq.RangeAsync(0, 5).SelectAsync(x => new DummyIndexValue(x, x * 2)).AppendAsync(new DummyIndexValue(1, -1)).ToDictionaryAsync(x => x.Index, x => x.Value);
        (await sut.Should().ThrowAsync<ArgumentException>()).And.Message.Should().Be("An item with the same key has already been added. Key: 1");
    }

    [Fact]
    public async Task ToDictionaryAsync_should_enumerate_each_item()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.ToDictionaryAsync(x => x);
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task ToDictionaryAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.ToDictionaryAsync(y => y);
        await sut.Should_enumerate_each_item_once();
    }
}
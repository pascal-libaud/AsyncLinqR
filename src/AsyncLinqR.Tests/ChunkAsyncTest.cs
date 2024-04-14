namespace AsyncLinqR.Tests;

public class ChunkAsyncTest
{
    [Fact]
    public async Task ChunkAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(10).ChunkAsync(3).ToListAsync();

        Assert.Equal(4, result.Count);

        Assert.Equal(result[0], new List<int> { 0, 1, 2 });
        Assert.Equal(result[1], new List<int> { 3, 4, 5 });
        Assert.Equal(result[2], new List<int> { 6, 7, 8 });
        Assert.Equal(result[3], new List<int> { 9 });
    }

    [Fact]
    public async Task ChunkAsync_should_work_with_empty_source()
    {
        Assert.Equal(0, await AsyncLinq.EmptyAsync<int>().ChunkAsync(3).CountAsync());
    }

    [Fact]
    public async Task ChunkAsync_should_not_return_last_empty_chunk()
    {
        Assert.Equal(3, await AsyncLinq.RangeAsync(9).ChunkAsync(3).CountAsync());
    }

    [Fact]
    public async Task ChunkAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.ChunkAsync(3);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ChunkAsync_should_enumerate_each_item_once()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.ChunkAsync(3).ToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task ChunkAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RangeAsync(10).ChunkAsync(3, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ChunkAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RangeAsync(10).ChunkAsync(3).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

}
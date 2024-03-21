namespace AsyncLinqR.Tests;

public class DistinctAsyncTest
{
    private static SpyAsyncEnumerable<int> GetSpy()
    {
        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        return SpyAsyncEnumerable.GetValuesAsync(source);
    }

    [Fact]
    public async Task DistinctAsync_should_work_as_expected()
    {
        var spy = GetSpy();

        var actual = await spy.DistinctAsync().ToListAsync();
        actual.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
    }

    [Fact]
    public async Task DistinctAsync_should_enumerate_each_item_once()
    {
        var spy = GetSpy();

        _ = await spy.DistinctAsync().ToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task DistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var func = async () => await source.DistinctAsync(token.Token).ToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact] 
    public async Task DistinctAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var func = async () => await source.DistinctAsync().ToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
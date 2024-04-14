namespace AsyncLinqR.Tests;

public class SkipWhileAsyncTest
{
    [Fact]
    public async Task SkipWhileAsync_should_work_as_expected()
    {
        Assert.Equal([5, 6, 7, 8, 9], await AsyncLinq.RangeAsync(0, 10).SkipWhileAsync(i => i != 5).ToListAsync());
    }

    [Fact]
    public async Task SkipWhileAsync_should_not_reorder_items()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipWhileAsync(_ => false).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task SkipWhileAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.SkipWhileAsync(_ => false).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task SkipWhileAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.SkipWhileAsync(_ => false);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task SkipWhileAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipWhileAsync(_ => false, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task SkipWhileAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipWhileAsync(_ => false).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
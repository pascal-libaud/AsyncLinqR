namespace AsyncLinqR.Tests;

public class TakeWhileAsyncTest
{
    [Fact]
    public async Task TakeWhileAsync_should_work_as_expected()
    {
        Assert.Equal([0, 1, 2, 3, 4], await AsyncLinq.RangeAsync(0, 10).TakeWhileAsync(i => i != 5).ToListAsync());
    }

    [Fact]
    public async Task TakeWhileAsync_should_not_reorder_items()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).TakeWhileAsync(_ => true).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task TakeWhileAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.TakeWhileAsync(_ => true).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task TakeWhileAsync_should_not_enumerate_all_when_found_predicate_true()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.TakeWhileAsync(x => x != 2).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task TakeWhileAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.TakeWhileAsync(_ => true);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task TakeWhileAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.TakeWhileAsync(_ => true, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task TakeWhileAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.TakeWhileAsync(_ => true).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
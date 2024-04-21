namespace AsyncLinqR.Tests;

public class SkipLastAsyncTest
{
    [Fact]
    public void SkipLastAsync_should_return_empty_when_count_too_big()
    {
        Assert.Empty(AsyncLinq.RangeAsync(10).SkipLastAsync(10));
        Assert.Empty(AsyncLinq.RangeAsync(10).SkipLastAsync(12));
    }

    [Fact]
    public async Task SkipLastAsync_should_return_only_one_item_when_count_equals_count_minus_one()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipLastAsync(9).ToListAsync(), [0]);
    }

    [Fact]
    public async Task SkipLastAsync_should_not_reorder_items()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipLastAsync(0).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task SkipLastAsync_should_not_change_anything_when_count_equals_zero()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipLastAsync(0).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task SkipLastAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.SkipLastAsync(1).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task SkipLastAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.SkipLastAsync(1);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task SkipLastAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipLastAsync(2, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task SkipLastAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipLastAsync(2).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
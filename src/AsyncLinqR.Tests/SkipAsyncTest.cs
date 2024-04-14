namespace AsyncLinqR.Tests;

public class SkipAsyncTest
{
    [Fact]
    public void SkipAsync_should_return_empty_when_count_too_big()
    {
        Assert.Empty(AsyncLinq.RangeAsync(10).SkipAsync(10));
        Assert.Empty(AsyncLinq.RangeAsync(10).SkipAsync(12));
    }

    [Fact]
    public async Task SkipAsync_should_return_only_one_item_when_count_equals_count_minus_one()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipAsync(9).ToListAsync(), [9]);
    }

    [Fact]
    public async Task SkipAsync_should_not_reorder_items()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipAsync(0).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task SkipAsync_should_not_change_anything_when_count_equals_zero()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).SkipAsync(0).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task SkipAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipAsync(2, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task SkipAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SkipAsync(2).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
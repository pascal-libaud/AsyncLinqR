namespace AsyncLinqR.Tests;

public class AnyAsyncTest
{
    [Fact]
    public async Task AnyAsync_should_return_false_when_no_item_matches()
    {
        Assert.False(await AsyncLinq.RangeAsync(0, 10).AnyAsync(x => x > 10));
    }

    [Fact]
    public async Task AnyAsync_should_return_true_when_at_least_one_item_matches()
    {
        Assert.True(await AsyncLinq.RangeAsync(0, 10).AnyAsync(x => x == 2));
    }

    [Fact]
    public async Task AnyAsync_should_return_false_when_empty()
    {
        Assert.False(await AsyncLinq.EmptyAsync<int>().AnyAsync(x => x == 10));
        Assert.False(await AsyncLinq.EmptyAsync<int>().AnyAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task AnyAsync_should_not_enumerate_all_when_one_true()
    {
        var sut = async (IAsyncEnumerable<int> x) => await x.AnyAsync(z => z == 8);
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task AnyAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RangeAsync(10).AnyAsync(x => x != 4, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task AnyAsync_overload_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RangeAsync(10).AnyAsync(x => (x != 4).ToTask(), token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task AnyAsync_overload_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await RangeHelper.Range(10).AnyAsync(x => (x != 4).ToTask(), token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
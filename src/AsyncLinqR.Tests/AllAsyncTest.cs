namespace AsyncLinqR.Tests;

public class AllAsyncTest
{
    [Fact]
    public async Task AllAsync_should_return_true_when_all_item_match()
    {
        Assert.True(await AsyncLinq.RangeAsync(0, 10).AllAsync(x => x < 10));
    }

    [Fact]
    public async Task AllAsync_should_return_false_when_at_least_one_item_does_not_match()
    {
        Assert.False(await AsyncLinq.RangeAsync(0, 10).AllAsync(x => x != 2));
    }

    [Fact]
    public async Task AllAsync_should_return_true_when_empty()
    {
        Assert.True(await AsyncLinq.EmptyAsync<int>().AllAsync(x => x == 10));
        Assert.True(await AsyncLinq.EmptyAsync<int>().AllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task AllAsync_should_not_enumerate_all_when_one_false()
    {
        var omAll = async (IAsyncEnumerable<int> x) => await x.AllAsync(z => z < 8);
        await omAll.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task AllAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await AsyncLinq.RangeAsync(10).AllAsync(x => x == 4, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    } 
    
    [Fact]
    public async Task AllAsync_overload_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await AsyncLinq.RangeAsync(10).AllAsync(x =>  (x == 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task AllAsync_overload_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await RangeHelper.Range(10).AllAsync(x => (x == 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
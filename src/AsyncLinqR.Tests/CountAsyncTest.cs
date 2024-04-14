namespace AsyncLinqR.Tests;

public class CountAsyncTest
{
    [Fact]
    public async Task CountAsync_should_work_as_expected()
    {
        Assert.Equal(10, await AsyncLinq.RangeAsync(10).CountAsync());
    }

    [Fact]
    public async Task CountAsync_should_enumerate_only_once()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.CountAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task DistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RangeAsync(10).CountAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task DistinctAsync_should_pass_cancellation_token_bis()
    { 
        IAsyncEnumerable<int> GetValues()
        {
            return AsyncLinq.RangeAsync(10);
        }

        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await GetValues().CountAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
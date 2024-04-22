namespace AsyncLinqR.Tests;

public class RepeatAsyncTest
{
    [Fact]
    public async Task RepeatAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RepeatAsync("Patin", 2).ToListAsync();
        var expected = new List<string> { "Patin", "Patin" };

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task RepeatAsync_should_be_empty_when_count_is_zero()
    {
        Assert.Empty(await AsyncLinq.RepeatAsync("Patin", 0).ToListAsync());
    }

    [Fact]
    public async Task RepeatAsync_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RepeatAsync("Patin", 2, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RepeatAsync_should_receive_cancellation_token_and_cancel()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await AsyncLinq.RepeatAsync("Patin", 2, token.Token).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
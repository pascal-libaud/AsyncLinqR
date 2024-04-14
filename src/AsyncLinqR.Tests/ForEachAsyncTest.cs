namespace AsyncLinqR.Tests;

public class ForEachAsyncTest
{
    [Fact]
    public async Task ForEachAsync_should_work_as_expected()
    {
        int count = 0;
        await AsyncLinq.RangeAsync(10).ForEachAsync(_ => count++);

        Assert.Equal(10, count);
    }

    [Fact]
    public async Task ForEachAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.ForEachAsync(_ => { }, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
namespace AsyncLinqR.Tests;

public class ReverseAsyncTest
{
    [Fact]
    public async Task ReverseAsync_should_reverse_alphabet()
    {
        var expected = new[] { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N',
            'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A' };
        Assert.Equal(expected, await RangeHelper.Alphabet().ToAsyncEnumerable().ReverseAsync().ToArrayAsync());
    }

        [Fact]
    public async Task DistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = RangeHelper.Alphabet().ToAsyncEnumerable();
        var sut = async () => await source.ReverseAsync(token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
    [Fact] 
    public async Task DistinctAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = RangeHelper.Alphabet().ToAsyncEnumerable();
        var sut = async () => await source.ReverseAsync().ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
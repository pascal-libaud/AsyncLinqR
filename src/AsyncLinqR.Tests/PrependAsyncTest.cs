using System.Runtime.CompilerServices;

namespace AsyncLinqR.Tests;

public class PrependAsyncTest
{
    static async IAsyncEnumerable<string> GetValuesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Yield();
        yield return "P";
        yield return "Q";
    }

    [Fact]
    public async Task PrependAsync_should_add_in_end_when_source_not_null()
    {
        Assert.Equal(new[] { "O", "P", "Q" }, await GetValuesAsync().PrependAsync("O").ToListAsync());
    }

    [Fact]
    public async Task PrependAsync_should_work_well_when_source_empty()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield break;
        }

        Assert.Equal(new[] { "R" }, await GetValuesAsync().PrependAsync("R").ToListAsync());
    }

    [Fact]
    public async Task PrependAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var sut = async () => await source.PrependAsync("o", token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task PrependAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var sut = async () => await source.PrependAsync("O").ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
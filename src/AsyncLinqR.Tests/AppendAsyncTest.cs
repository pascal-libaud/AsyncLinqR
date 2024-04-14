using System.Runtime.CompilerServices;

namespace AsyncLinqR.Tests;

public class AppendAsyncTest
{
    static async IAsyncEnumerable<string> GetValuesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Yield();
        yield return "P";
        yield return "Q";
    }

    [Fact]
    public async Task AppendAsync_should_add_in_end_when_source_not_null()
    {

        Assert.Equal(new[] { "P", "Q", "R" }, await GetValuesAsync().AppendAsync("R").ToListAsync());
    }

    [Fact]
    public async Task AppendAsync_should_work_well_when_source_empty()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield break;
        }

        Assert.Equal(new[] { "R" }, await GetValuesAsync().AppendAsync("R").ToListAsync());
    }

    [Fact]
    public async Task AppendAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var sut = async () => await source.AppendAsync("R", token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task AppendAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var sut = async () => await source.AppendAsync("R").ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
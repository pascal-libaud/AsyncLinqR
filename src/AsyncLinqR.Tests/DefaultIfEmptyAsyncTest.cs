#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
namespace AsyncLinqR.Tests;

public class DefaultIfEmptyAsyncTest
{
    [Fact]
    public async Task DefaultIfEmptyAsync_should_work_as_expected_on_filled_source()
    {
        var source = new List<int> { 0, 1, 2, 3 }.ToAsyncEnumerable();
        var actual = await source.DefaultIfEmptyAsync().ToListAsync();
        actual.Should().BeEquivalentTo(new List<int?> { 0, 1, 2, 3 });
    }

    [Fact]
    public async Task DefaultIfEmptyAsync_should_work_as_expected_on_empty_source()
    {
        var source = AsyncLinq.EmptyAsync<int>();
        var actual = await source.DefaultIfEmptyAsync().ToListAsync();
        actual.Should().BeEquivalentTo(new List<int?> { 0 });
    }

    [Fact]
    public async Task DefaultIfEmptyAsync_should_throw_exception_when_source_null()
    {
        IAsyncEnumerable<int>? source = null;
        var sut = async () => await source.DefaultIfEmptyAsync().ToListAsync();
        (await sut.Should().ThrowAsync<ArgumentNullException>()).And.Message.Should().Be("Value cannot be null. (Parameter 'source')");
    }

    [Fact]
    public async Task DefaultIfEmptyAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.DefaultIfEmptyAsync().ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task DefaultIfEmptyAsync_should_enumerate_all_when_break()
    {
        var sut = (IAsyncEnumerable<int> x) => x.DefaultIfEmptyAsync().TakeAsync(2).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task DefaultIfEmptyAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.DefaultIfEmptyAsync();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task DefaultIsEmptyAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.DefaultIfEmptyAsync(token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task DefaultIsEmptyAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.DefaultIfEmptyAsync().ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
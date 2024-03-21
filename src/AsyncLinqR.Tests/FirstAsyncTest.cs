namespace AsyncLinqR.Tests;

public class FirstAsyncTest
{
    [Fact]
    public async Task FirstAsync_should_work_as_expected()
    {
        var source = AsyncLinq.RangeAsync(0, 10)
            .SelectAsync(x => new DummyIndexValue(x, x))
            .ConcatAsync(AsyncLinq.RangeAsync(0, 10).SelectAsync(x => new DummyIndexValue(x + 10, x)));

        var result = await source.FirstAsync(x => x.Value == 5);
        Assert.Equal(new DummyIndexValue(5, 5), result);
    }

    [Fact]
    public async Task FirstAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().FirstAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().FirstAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task FirstAsync_without_predicate_should_not_enumerate_all()
    {
        var omFirstAsync = (IAsyncEnumerable<int> x) => x.FirstAsync();
        await omFirstAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task FirstAsync_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirstAsync = (IAsyncEnumerable<int> x) => x.FirstAsync(z => z == 5);
        await omFirstAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task FirstAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => AsyncLinq.EmptyAsync<int>().FirstAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task FirstAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => AsyncLinq.RangeNullableAsync(0, 10).FirstAsync(x => x == 20);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task FirstAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().FirstAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
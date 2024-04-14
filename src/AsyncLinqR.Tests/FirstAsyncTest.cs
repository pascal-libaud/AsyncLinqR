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
        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().FirstAsync(x => x == 2);
        await sut.Should().NotThrowAsync();

        sut = () => new List<int> { 1, 2 }.ToAsyncEnumerable().FirstAsync();
        await sut.Should().NotThrowAsync();
    }

    [Fact]
    public async Task FirstAsync_without_predicate_should_not_enumerate_all()
    {
        var sut = (IAsyncEnumerable<int> x) => x.FirstAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task FirstAsync_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var sut = (IAsyncEnumerable<int> x) => x.FirstAsync(z => z == 5);
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task FirstAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.EmptyAsync<int>().FirstAsync();
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task FirstAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.RangeNullableAsync(0, 10).FirstAsync(x => x == 20);
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task FirstAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().FirstAsync(x => x == 2, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
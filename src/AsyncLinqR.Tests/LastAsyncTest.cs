namespace AsyncLinqR.Tests;

public class LastAsyncTest
{
    [Fact]
    public async Task LastAsync_should_work_as_expected()
    {
        var source = AsyncLinq.RangeAsync(0, 10)
            .SelectAsync(x => new DummyIndexValue(x, x))
            .ConcatAsync(AsyncLinq.RangeAsync(0, 10).SelectAsync(x => new DummyIndexValue(x + 10, x)));

        var result = await source.LastAsync(x => x.Value == 5);
        Assert.Equal(new DummyIndexValue(15, 5), result);
    }

    [Fact]
    public async Task LastAsync_should_not_throw_when_found_multiple_candidates()
    {
        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastAsync(x => x == 2);
        await sut.Should().NotThrowAsync();

        sut = () => new List<int> { 1, 2 }.ToAsyncEnumerable().LastAsync();
        await sut.Should().NotThrowAsync();
    }

    // TODO Voir comment en faire une méthode d'extension
    [Fact]
    public async Task LastAsync_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.LastAsync();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task LastAsync_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();
        
        _ = await spy.LastAsync(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task LastAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.EmptyAsync<int>().LastAsync();
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task LastAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.RangeNullableAsync(0, 10).LastAsync(x => x == 20);
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task LastAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastAsync(x => x == 2, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
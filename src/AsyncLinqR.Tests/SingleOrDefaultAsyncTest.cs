namespace AsyncLinqR.Tests;

public class SingleOrDefaultAsyncTest
{
    [Fact]
    public async Task SingleOrDefaultAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(0, 10).SingleOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_should_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().SingleOrDefaultAsync(x => x == 2);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one matching element");

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().SingleOrDefaultAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    [Fact]
    public async Task SingleOrDefaultAsync_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.SingleOrDefaultAsync(x => x == 0);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.EmptyAsync<int?>().SingleOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await AsyncLinq.RangeAsync(5, 5).SingleOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.RangeNullableAsync(0, 10).SingleOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => AsyncLinq.EmptyAsync<int?>().SingleOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SingleOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => AsyncLinq.RangeNullableAsync(0, 10).SingleOrDefaultAsync(x => x == 20);
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SingleOrDefaultAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().SingleOrDefaultAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
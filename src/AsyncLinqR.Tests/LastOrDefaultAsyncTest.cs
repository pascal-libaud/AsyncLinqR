namespace AsyncLinqR.Tests;

public class LastOrDefaultAsyncAsyncTest
{
    [Fact]
    public async Task LastOrDefaultAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(0, 10).LastOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastOrDefaultAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().LastOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.LastOrDefaultAsync();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.LastOrDefaultAsync(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.EmptyAsync<int?>().LastOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await AsyncLinq.RangeAsync(5, 5).LastOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.RangeNullableAsync(0, 10).LastOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => AsyncLinq.EmptyAsync<int?>().LastOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => AsyncLinq.RangeAsync(0, 10).LastOrDefaultAsync(x => x == 20);
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastOrDefaultAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

}
namespace AsyncLinqR.Tests;

public class HasDuplicateAsyncTest
{
    [Fact]
    public async Task HasDuplicateAsync_should_work_as_expected()
    {
        Assert.False(await AsyncLinq.EmptyAsync<int>().HasDuplicateAsync());

        var source = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Assert.False(await source.ToAsyncEnumerable().HasDuplicateAsync());

        source.Add(3);
        Assert.True(await source.ToAsyncEnumerable().HasDuplicateAsync());
    }

    [Fact]
    public async Task HasDuplicateAsync_should_enumerate_each_item_once()
    {
        var hasDuplicateAsync = (IAsyncEnumerable<int> x) => x.HasDuplicateAsync();
        await hasDuplicateAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task HasDuplicateAsync_should_not_enumerate_all_when_found_duplicate()
    {
        var spy = new SpyAsyncEnumerable<int>(new List<int> { 0, 1, 2, 2, 3, 4 }.ToAsyncEnumerable());
        var hasDuplicateAsync = (IAsyncEnumerable<int> x) => x.HasDuplicateAsync();
        await hasDuplicateAsync.Should_enumerate_each_item_once(spy);
    }

    [Fact]
    public async Task HasDuplicateAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = async () => await SpyAsyncEnumerable.GetValuesAsync().HasDuplicateAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
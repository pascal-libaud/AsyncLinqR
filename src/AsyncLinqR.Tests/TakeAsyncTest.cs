namespace AsyncLinqR.Tests;

public class TakeAsyncTest
{
    [Fact]
    public async Task TakeAsync_should_return_all_when_count_too_big()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).ToListAsync(), await AsyncLinq.RangeAsync(10).TakeAsync(10).ToListAsync());
        Assert.Equal(await AsyncLinq.RangeAsync(10).ToListAsync(), await AsyncLinq.RangeAsync(10).TakeAsync(12).ToListAsync());
    }

    [Fact]
    public async Task TakeAsync_should_return_the_number_of_items_when_count_not_greater_than_size_of_source()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).TakeAsync(5).ToListAsync(), [0, 1, 2, 3, 4]);
    }

    [Fact]
    public async Task TakeAsync_should_not_reorder_items()
    {
        Assert.Equal(await AsyncLinq.RangeAsync(10).TakeAsync(10).ToListAsync(), await AsyncLinq.RangeAsync(10).ToListAsync());
    }

    [Fact]
    public async Task TakeAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.TakeAsync(1).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task TakeAsync_should_not_enumerate_all_when_count_less_than_source_size()
    {
        var sut = async (IAsyncEnumerable<int> x) => await x.TakeAsync(1).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task TakeAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.TakeAsync(1);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task TakeAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.TakeAsync(2, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task TakeAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.TakeAsync(2).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
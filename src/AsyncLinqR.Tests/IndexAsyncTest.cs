#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
namespace AsyncLinqR.Tests;

public class IndexAsyncTest
{
    [Fact]
    public async Task IndexAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.IndexAsync();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task IndexAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.IndexAsync().ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task IndexAsync_should_not_make_stack_overflow()
    {

        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.IndexAsync().TakeAsync(5).ToListAsync();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void IndexAsync_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IAsyncEnumerable<int>? enumerable = null;
            _ = enumerable.IndexAsync();
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public async Task IndexAsync_should_throw_when_null_on_enumeration()
    {
        var sut = async () =>
        {
            IAsyncEnumerable<int>? enumerable = null;
            await foreach (var _ in enumerable.IndexAsync())
            { }
        };

        await Assert.ThrowsAsync<NullReferenceException>(sut);
    }

    [Fact]
    public async Task IndexAsync_should_work_as_expected_when_not_null()
    {
        var source = await AsyncLinq.RangeAsync(5).SelectAsync(x => x * 2).IndexAsync().ToListAsync();
        var expected = new[] { (0, 0), (1, 2), (2, 4), (3, 6), (4, 8) };

        Assert.Equal(expected, source);
    }

    [Fact]
    public async Task IndexAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.IndexAsync(token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task IndexAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.IndexAsync().ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
namespace AsyncLinqR.Tests;

public class SelectAsyncTest
{
    [Fact]
    public async Task SelectAsync_should_not_enumerate_early()
    {
        // TODO Voir si on peut utiliser TestHelper.Verify_method_should_not_enumerate_early
        var spy = SpyEnumerable.GetValues();

        var source = spy.SelectAsync(x => x.ToString().ToTask());
        Assert.False(spy.IsEnumerated);

        _ = await source.TakeAsync(5).ToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public async Task SelectAsync_should_enumerate_each_item_once()
    {
        var sut = async (IAsyncEnumerable<int> x) => await x.SelectAsync(z => z.ToString().ToTask()).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task SelectAsync_should_not_make_stack_overflow()
    {
        var spy = SpyEnumerable.GetValues();

        _ = await spy.SelectAsync(x => x.ToString().ToTask()).TakeAsync(5).ToListAsync();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void SelectAsync_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IAsyncEnumerable<int>? enumerable = null;
            _ = enumerable.SelectAsync(x => x.ToString());
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public async Task SelectAsync_should_throw_when_null_on_enumeration()
    {
        var sut = async () =>
        {
            IEnumerable<int>? enumerable = null;
            await foreach (var _ in enumerable.SelectAsync(x => x.ToString().ToTask()))
            { }
        };

        await Assert.ThrowsAsync<NullReferenceException>(sut);
    }

    [Fact]
    public async Task SelectAsync_should_work_well_when_not_null()
    {
        var source = await RangeHelper.Range(5).SelectAsync(x => (x * 2).ToTask()).ToListAsync();
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }

    [Fact]
    public async Task SelectAsync_with_index_should_have_got_right_indexes()
    {
        Assert.All(await AsyncLinq.RangeAsync(10).SelectAsync((_, i) => i).ToListAsync(), (x, i) => Assert.Equal(x, i));
    }

    [Fact]
    public async Task SelectAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SelectAsync(x => x, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task SelectAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var sut = async () => await source.SelectAsync(x => x).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
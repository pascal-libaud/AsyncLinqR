namespace AsyncLinqR.Tests;

public class CastAsyncTest
{
    [Fact]
    public async Task CastAsync_should_return_as_many_elements_as_the_source_list()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy1(), new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var result = await source.CastAsync<DummyBase, Dummy1>().ToListAsync();
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task CastAsync_should_throw_an_exception_when_cast_is_invalid()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var action = () =>  source.CastAsync<DummyBase, Dummy1>().ToListAsync();
        await action.Should().ThrowAsync<InvalidCastException>();
    }

    [Fact]
    public async Task CastAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.CastAsync<int, int>().ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task CastAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.CastAsync<int, int>();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task CastAsync_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IAsyncEnumerable<int> x) => x.CastAsync<int, int>().TakeAsync(2).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task CastAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var func = async () => await source.CastAsync<DummyBase, Dummy1>(token.Token).ToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task CastAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var func = async () => await source.CastAsync<DummyBase, Dummy1>().ToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
namespace AsyncLinqR.Tests;

public class OfTypeAsyncTest
{
    [Fact]
    public async Task OfTypeAsync_should_return_only_matching_elements()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var result = await source.OfTypeAsync<DummyBase, Dummy1>().ToListAsync();

        result.Should().BeEquivalentTo(new List<Dummy1> { new(), new() }, x => x.ComparingRecordsByValue());
    }

    [Fact]
    public async Task OfTypeAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OfTypeAsync<int, int>().ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task OfTypeAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OfTypeAsync<int, int>();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task OfTypeAsync_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OfTypeAsync<int, int>().TakeAsync(2).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OfTypeAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var func = async () => await source.OfTypeAsync<DummyBase, Dummy1>(token.Token).ToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OfTypeAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var func = async () => await source.OfTypeAsync<DummyBase, Dummy1>().ToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
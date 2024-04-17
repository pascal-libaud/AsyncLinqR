namespace AsyncLinqR.Tests;

public class UnionAsyncTest
{
    [Fact]
    public async Task Union_should_work_as_expected_1()
    {
        var list1 = new List<Dummy> { new(1), new(2), new(3), }.ToAsyncEnumerable();
        var list2 = new List<Dummy> { new(2), new(3), new(4), }.ToAsyncEnumerable();

        var expected = new List<Dummy> { new(1), new(2), new(3), new(4), };

        var result = await list1.UnionAsync(list2).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Union_should_work_as_expected_3()
    {
        var list1 = new List<Dummy> { new(1), new(2), new(1), new(2), }.ToAsyncEnumerable();
        var list2 = new List<Dummy> { new(2), new(2), new(2), }.ToAsyncEnumerable();

        var expected = new List<Dummy> { new(1), new(2), };

        var result = await list1.UnionAsync(list2).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UnionAsync_should_not_enumerate_early_on_first_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => x.UnionAsync(new List<int> { 1, 2 }.ToAsyncEnumerable());
        await unionAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task UnionAsync_should_not_enumerate_early_on_second_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().UnionAsync(x);
        await unionAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task UnionAsync_should_enumerate_each_ite_once_on_first_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => x.UnionAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).ToListAsync();
        await unionAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task UnionAsync_should_enumerate_each_ite_once_on_second_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().UnionAsync(x).ToListAsync();
        await unionAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task Union_should_not_enumerable_all_when_break_on_first_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => x.UnionAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).TakeAsync(2).ToListAsync();
        await unionAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task Union_should_not_enumerable_all_when_break_on_second_enumerable()
    {
        var unionAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().UnionAsync(x).TakeAsync(1).ToListAsync();
        await unionAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task UnionAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.UnionAsync(second, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task UnionAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.UnionAsync(second).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}

file record Dummy(int Value);
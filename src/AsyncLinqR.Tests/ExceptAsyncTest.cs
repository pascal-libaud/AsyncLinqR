namespace AsyncLinqR.Tests;

public class ExceptAsyncTest
{
    [Fact]
    public async Task ExceptAsync_should_work_as_expected_1()
    {
        var list1 = new List<Dummy> { new(1), new(2), new(3), }.ToAsyncEnumerable();
        var list2 = new List<Dummy> { new(2), new(3), new(4), }.ToAsyncEnumerable();

        var expected = new List<Dummy> { new(1), };

        var result = await list1.ExceptAsync(list2).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ExceptAsync_should_work_as_expected_2()
    {
        var list1 = new List<Dummy> { new(1), new(2), new(1), new(2), }.ToAsyncEnumerable();
        var list2 = new List<Dummy> { new(2), new(2), new(2), }.ToAsyncEnumerable();

        var expected = new List<Dummy> { new(1), };

        var result = await list1.ExceptAsync(list2).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ExceptAsync_should_not_enumerate_early_on_first_enumerable()
    {
        var exceptAsync = (IAsyncEnumerable<int> x) => x.ExceptAsync(new List<int> { 1, 2 }.ToAsyncEnumerable());
        await exceptAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ExceptAsync_should_not_enumerate_early_on_second_enumerable()
    {
        var exceptAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().ExceptAsync(x);
        await exceptAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ExceptAsync_should_enumerate_each_ite_once_on_first_enumerable()
    {
        var exceptAsync = (IAsyncEnumerable<int> x) => x.ExceptAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).ToListAsync();
        await exceptAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ExceptAsync_should_enumerate_each_ite_once_on_second_enumerable()
    {
        var exceptAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().ExceptAsync(x).ToListAsync();
        await exceptAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ExceptAsync_should_not_enumerable_all_when_break_on_first_enumerable()
    {
        var exceptAsync = (IAsyncEnumerable<int> x) => x.ExceptAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).TakeAsync(2).ToListAsync();
        await exceptAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task ExceptAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ExceptAsync(second, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ExceptAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ExceptAsync(second).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}

file record Dummy(int Value);
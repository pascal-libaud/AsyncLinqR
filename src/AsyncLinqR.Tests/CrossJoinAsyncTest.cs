namespace AsyncLinqR.Tests;

public class CrossJoinAsyncTest
{
    [Fact]
    public async Task CrossJoin_should_work_as_expected()
    {
        var first = new List<First> { new(1), new(2), new(3), }.ToAsyncEnumerable();
        var second = new List<Second> { new(2), new(4), new(6), }.ToAsyncEnumerable();

        var expected = new List<Result> { new(1, 2), new(1, 4), new(1, 6), new(2, 2), new(2, 4), new(2, 6), new(3, 2), new(3, 4), new(3, 6), };

        var result = await first.CrossJoinAsync(second, (x, y) => new Result(x.Key, y.Key)).ToListAsync();
        result.Should().HaveCount(9);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CrossJoinAsync_should_not_enumerate_early_on_first_enumerable()
    {
        var sut = (IAsyncEnumerable<int> first) => first.CrossJoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), (x, y) => x + y);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task CrossJoinAsync_should_not_enumerate_early_on_second_enumerable()
    {
        var sut = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().CrossJoinAsync(second, (x, y) => x + y);
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task CrossJoinAsync_should_enumerate_each_ite_once_on_first_enumerable()
    {
        var sut = (IAsyncEnumerable<int> first) => first.CrossJoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), (x, y) => x + y).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task CrossJoinAsync_should_enumerate_each_ite_once_on_second_enumerable()
    {
        var sut = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().CrossJoinAsync(second, (x, y) => x + y).ToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task CrossJoinAsync_should_not_enumerable_all_when_break_on_first_enumerable()
    {
        var sut = (IAsyncEnumerable<int> second) => second.CrossJoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), (x, y) => x + y).TakeAsync(2).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    // Trop lourd à mettre en place et Linq ne le gère pas non plus
    //[Fact]
    public async Task CrossJoinAsync_should_not_enumerable_all_when_break_on_second_enumerable()
    {
        var sut = (IAsyncEnumerable<int> first) => new List<int> { 1, 2 }.ToAsyncEnumerable().CrossJoinAsync(first, (x, y) => x + y).TakeAsync(1).ToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task CrossJoinAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.CrossJoinAsync(second, (x, y) => x + y, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task CrossJoinAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.CrossJoinAsync(second, (x, y) => x + y).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}

file record First(int Key);
file record Second(int Key);
file record Result(int FirstKey, int SecondKey);
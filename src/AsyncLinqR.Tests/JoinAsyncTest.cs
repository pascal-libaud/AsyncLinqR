namespace AsyncLinqR.Tests;

public class JoinAsyncTest
{
    [Fact]
    public async Task Join_should_work_as_expected_1()
    {
        var list1 = new List<Outer> { new(1), new(2), new(3), new(4), new(5) }.ToAsyncEnumerable();
        var list2 = new List<Inner> { new(2), new(4), new(6), new(8) }.ToAsyncEnumerable();

        var expected = new List<Result> { new(2, 2), new(4, 4) };

        var result = await list1.JoinAsync(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).ToListAsync();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task JoinAsync_should_work_as_expected_2()
    {
        var list1 = new List<Outer> { new(1), new(2), new(1), new(2) }.ToAsyncEnumerable();
        var list2 = new List<Inner> { new(2), new(1) }.ToAsyncEnumerable();

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = await list1.JoinAsync(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).ToListAsync();
        result.Should().HaveCount(4);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task JoinAsync_should_work_as_expected_3()
    {
        var list1 = new List<Outer> { new(1), new(2) }.ToAsyncEnumerable();
        var list2 = new List<Inner> { new(2), new(1), new(2), new(1) }.ToAsyncEnumerable();

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = await list1.JoinAsync(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).ToListAsync();
        result.Should().HaveCount(4);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task JoinAsync_should_work_as_expected_4()
    {
        var list1 = new List<Outer> { new(1), new(2), new(1), new(2) }.ToAsyncEnumerable();
        var list2 = new List<Inner> { new(2), new(1), new(2), new(1) }.ToAsyncEnumerable();

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2), new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = await list1.JoinAsync(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).ToListAsync();
        result.Should().HaveCount(8);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task JoinAsync_should_not_enumerate_early_on_outer_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => x.JoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), y => y, y => y, (y, z) => y + z);
        await joinAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task JoinAsync_should_not_enumerate_early_on_inner_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().JoinAsync(x, y => y, y => y, (y, z) => y + z);
        await joinAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task JoinAsync_should_enumerate_each_ite_once_on_outer_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => x.JoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), y => y, y => y, (y, z) => y + z).ToListAsync();
        await joinAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task JoinAsync_should_enumerate_each_ite_once_on_inner_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().JoinAsync(x, y => y, y => y, (y, z) => y + z).ToListAsync();
        await joinAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task Join_should_not_enumerable_all_when_break_on_outer_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => x.JoinAsync(new List<int> { 1, 2 }.ToAsyncEnumerable(), y => y, y => y, (y, z) => y + z).TakeAsync(2).ToListAsync();
        await joinAsync.Should_not_enumerate_all_when();
    }

    // Trop lourd à mettre en place et Linq ne le gère pas non plus
    //[Fact]
    public async Task Join_should_not_enumerable_all_when_break_on_inner_enumerable()
    {
        var joinAsync = (IAsyncEnumerable<int> x) => new List<int> { 1, 2 }.ToAsyncEnumerable().JoinAsync(x, y => y, y => y, (y, z) => y + z).TakeAsync(1).ToListAsync();
        await joinAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task JoinAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var outer = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var inner = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await outer.JoinAsync(inner, x => x, x => x, (x, y) => x + y, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }


    [Fact]
    public async Task JoinAsync_should_receive_and_pass_cancellation_token_on_outer_enumerable()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var outer = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var inner = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await outer.JoinAsync(inner, x => x, x => x, (x, y) => x + y).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}

file record Outer(int Key);
file record Inner(int Key);
file record Result(int OuterKey, int InnerKey);
namespace AsyncLinqR.Tests;

public class ZipAsyncTest
{
    [Fact]
    public async Task ZipAsync_should_work_as_expected_1()
    {
        var first = new List<int> { 0, 1, 2, 3, 4 }.ToAsyncEnumerable();
        var second = new List<int> { 5, 6, 7 }.ToAsyncEnumerable();

        var expected = new List<int> { 5, 7, 9 };

        var result = await first.ZipAsync(second, (a, b) => a + b).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ZipAsync_should_work_as_expected_2()
    {
        var first = new List<int> { 5, 6, 7 }.ToAsyncEnumerable();
        var second = new List<int> { 0, 1, 2, 3, 4 }.ToAsyncEnumerable();

        var expected = new List<int> { 5, 7, 9 };

        var result = await first.ZipAsync(second, (a, b) => a + b).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ZipAsync_should_not_enumerate_early_on_first_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> first) => first.ZipAsync(new List<int> { 1, 2 }.ToAsyncEnumerable());
        await zipAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ZipAsync_should_not_enumerate_early_on_second_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipAsync(second);
        await zipAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ZipAsync_should_enumerate_each_ite_once_on_first_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> first) => first.ZipAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).ToListAsync();
        await zipAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ZipAsync_should_enumerate_each_ite_once_on_second_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipAsync(second).ToListAsync();
        await zipAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ZipAsync_should_not_enumerable_all_when_break_on_first_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> first) => first.ZipAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).TakeAsync(2).ToListAsync();
        await zipAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task ZipAsync_should_not_enumerable_all_when_break_on_second_enumerable()
    {
        var zipAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipAsync(second).TakeAsync(1).ToListAsync();
        await zipAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task ZipAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ZipAsync(second, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ZipAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ZipAsync(second).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
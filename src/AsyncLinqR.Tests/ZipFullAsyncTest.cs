namespace AsyncLinqR.Tests;

public class ZipFullAsyncTest
{
    [Fact]
    public async Task ZipFullAsync_should_work_as_expected_1()
    {
        var first = new List<int> { 0, 1, 2, 3, 4 }.ToAsyncEnumerable();
        var second = new List<int> { 5, 6, 7 }.ToAsyncEnumerable();

        var expected = new List<int> { 5, 7, 9, 3, 4 };

        var result = await first.ZipFullAsync(second, (a, b) => a + b).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ZipFullAsync_should_work_as_expected_2()
    {
        var first = new List<int> { 5, 6, 7 }.ToAsyncEnumerable();
        var second = new List<int> { 0, 1, 2, 3, 4 }.ToAsyncEnumerable();

        var expected = new List<int> { 5, 7, 9, 3, 4 };

        var result = await first.ZipFullAsync(second, (a, b) => a + b).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ZipFullAsync_should_work_as_expected_3()
    {
        var first = new List<int?> { 0, 1, 2, 3, 4 }.ToAsyncEnumerable();
        var second = new List<int?> { 5, 6, 7 }.ToAsyncEnumerable();

        var expected = new List<int?> { 5, 7, 9, null, null };

        var result = await first.ZipFullAsync(second, (a, b) => a + b).ToListAsync();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ZipFullAsync_should_not_enumerate_early_on_first_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> first) => first.ZipFullAsync(new List<int> { 1, 2 }.ToAsyncEnumerable());
        await zipfullAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ZipFullAsync_should_not_enumerate_early_on_second_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipFullAsync(second);
        await zipfullAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task ZipFullAsync_should_enumerate_each_ite_once_on_first_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> first) => first.ZipFullAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).ToListAsync();
        await zipfullAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ZipFullAsync_should_enumerate_each_ite_once_on_second_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipFullAsync(second).ToListAsync();
        await zipfullAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task ZipFullAsync_should_not_enumerable_all_when_break_on_first_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> first) => first.ZipFullAsync(new List<int> { 1, 2 }.ToAsyncEnumerable()).TakeAsync(2).ToListAsync();
        await zipfullAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task ZipFullAsync_should_not_enumerable_all_when_break_on_second_enumerable()
    {
        var zipfullAsync = (IAsyncEnumerable<int> second) => new List<int> { 1, 2 }.ToAsyncEnumerable().ZipFullAsync(second).TakeAsync(1).ToListAsync();
        await zipfullAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task ZipFullAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ZipFullAsync(second, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ZipFullAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var first = new List<int> { 1, 2 }.ToAsyncEnumerable();
        var second = new List<int> { 1, 2 }.ToAsyncEnumerable();

        var sut = async () => await first.ZipFullAsync(second).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
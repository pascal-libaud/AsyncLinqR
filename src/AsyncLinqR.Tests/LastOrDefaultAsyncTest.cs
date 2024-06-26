﻿using System.Linq;

namespace AsyncLinqR.Tests;

public class LastOrDefaultAsyncTest
{
    [Fact]
    public async Task LastOrDefaultAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(0, 10).LastOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_should_not_throw_when_found_multiple_candidates()
    {
        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastOrDefaultAsync(x => x == 2);
        await sut.Should().NotThrowAsync();

        sut = () => new List<int> { 1, 2 }.ToAsyncEnumerable().LastOrDefaultAsync();
        await sut.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_enumerate_all_when_first_demanded()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.LastOrDefaultAsync();
        await sut.Enumerate_all_when();
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_enumerate_all_when_first_demanded()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.LastOrDefaultAsync(x => x == 5);
        await sut.Enumerate_all_when();
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.EmptyAsync<int?>().LastOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await AsyncLinq.RangeAsync(5, 5).LastOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await AsyncLinq.RangeNullableAsync(0, 10).LastOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task LastOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var sut = () => AsyncLinq.EmptyAsync<int?>().LastOrDefaultAsync();
        await sut.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var sut = () => AsyncLinq.RangeAsync(0, 10).LastOrDefaultAsync(x => x == 20);
        await sut.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LastOrDefaultAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().LastOrDefaultAsync(x => x == 2, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
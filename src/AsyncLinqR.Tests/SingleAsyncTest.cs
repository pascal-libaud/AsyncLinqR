﻿namespace AsyncLinqR.Tests;

public class SingleAsyncTest
{
    [Fact]
    public async Task SingleAsync_should_work_as_expected()
    {
        var result = await AsyncLinq.RangeAsync(0, 10).SingleAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task SingleAsync_should_throw_when_found_multiple_candidates()
    {
        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().SingleAsync(x => x == 2);
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one matching element");

        sut = () => new List<int> { 1, 2 }.ToAsyncEnumerable().SingleAsync();
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    [Fact]
    public async Task SingleAsync_enumerate_all_when_first_demanded()
    {
        var sut = (IAsyncEnumerable<int> enumerable) => enumerable.SingleAsync(x => x == 0);
        await sut.Enumerate_all_when();
    }

    [Fact]
    public async Task SingleAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.EmptyAsync<int>().SingleAsync();
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task SingleAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var sut = () => AsyncLinq.RangeNullableAsync(0, 10).SingleAsync(x => x == 20);
        (await sut.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task SingleOrDefaultAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var sut = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().SingleAsync(x => x == 2, token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }
}
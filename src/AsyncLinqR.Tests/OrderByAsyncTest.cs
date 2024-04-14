namespace AsyncLinqR.Tests;

public class OrderByAsyncTest
{
    [Fact]
    public void OrderByAsync_should_sort_items()
    {
        var actual = new List<int> { 5, 4, 8, 1 }.ToAsyncEnumerable().OrderByAsync(x => x);
        var expected = new List<int> { 1, 4, 5, 8 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ThenByAsync_should_not_reorder_items_sorted_by_OrderByAsync()
    {
        Person t1 = new("Tata", 5);
        Person t2 = new("Toto", 10);
        Person t3 = new("Tata", 12);
        Person t4 = new("Toto", 9);

        var actual = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable().OrderByAsync(x => x.Name).ThenByAsync(x => x.Age);
        var expected = new List<Person> { t1, t3, t4, t2 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OrderByDescending_and_ThenByDescending_should_work_as_expected()
    {
        Person t1 = new("Tata", 5);
        Person t2 = new("Toto", 10);
        Person t3 = new("Tata", 12);
        Person t4 = new("Toto", 9);

        var actual = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable().OrderByAsync(x => x.Name).ThenByAsync(x => x.Age);
        List<Person> expected = [t1, t3, t4, t2];
        Assert.Equal(expected, actual);

        actual = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable().OrderByDescendingAsync(x => x.Name).ThenByAsync(x => x.Age);
        expected = [t4, t2, t1, t3];
        Assert.Equal(expected, actual);

        actual = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable().OrderByAsync(x => x.Name).ThenByDescendingAsync(x => x.Age);
        expected = [t3, t1, t2, t4];
        Assert.Equal(expected, actual);

        actual = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable().OrderByDescendingAsync(x => x.Name).ThenByDescendingAsync(x => x.Age);
        expected = [t2, t4, t3, t1];
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task OrderByAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 5, 4, 8, 1 }.ToAsyncEnumerable();
        var sut = async () => await source.OrderByAsync(x => x, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OrderByAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 5, 4, 8, 1 }.ToAsyncEnumerable();
        var sut = async () => await source.OrderByAsync(x => x).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ThenByAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        Person t1 = new("Tata", 5);
        Person t2 = new("Toto", 10);
        Person t3 = new("Tata", 12);
        Person t4 = new("Toto", 9);
        var source = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable();

        var sut = async () => await source.OrderByAsync(x => x.Name).ThenByAsync(x => x.Age, token.Token).ToListAsync();
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ThenByAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        Person t1 = new("Tata", 5);
        Person t2 = new("Toto", 10);
        Person t3 = new("Tata", 12);
        Person t4 = new("Toto", 9);
        var source = new List<Person> { t1, t2, t3, t4 }.ToAsyncEnumerable();

        var sut = async () => await source.OrderByAsync(x => x.Name).ThenByAsync(x => x.Age).ToListAsync(token.Token);
        await sut.Should().ThrowAsync<OperationCanceledException>();
    }

    // TODO
    //TestHelper.Should_not_enumerate_early()

    // TODO
    // TestHelper.Should_enumerate_each_item_once()
}

file record Person(string Name, int Age);
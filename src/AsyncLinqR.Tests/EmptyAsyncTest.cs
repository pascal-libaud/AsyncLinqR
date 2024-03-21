namespace AsyncLinqR.Tests;

public class EmptyAsyncTest
{
    [Fact]
    public void EmptyAsync_should_not_be_null()
    {
        var result = AsyncLinq.EmptyAsync<int>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task EmptyAsync_should_be_empty()
    {
        var result = await AsyncLinq.EmptyAsync<int>().ToListAsync();
        Assert.Empty(result);
    }
}
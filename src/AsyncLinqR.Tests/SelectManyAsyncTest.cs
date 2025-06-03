using System.Linq;

namespace AsyncLinqR.Tests;

public class SelectManyAsyncTest
{
    // TODO Renommer tous les tests pour donner des noms parlants
    [Fact]
    public async Task Suite()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.Create([]).SelectManyAsync(x => x.List).ToListAsync();
        array.Should().BeOfType<List<int>>();

        // TODO Rajouter List<int>
    }

    [Fact]
    public async Task SelectAsync_patin_couffin()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.CreateAsync([]).SelectManyAsync(x => x.List).ToListAsync();
        array.Should().BeOfType<List<int>>();

        // TODO Rajouter List<int>
    }

    [Fact]
    public async Task Selector_and_async_changer_ces_noms_pourris()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        // TODO Rajouter int[]
        // TODO Rajouter List<int>
    }

    [Fact]
    public async Task Selector_and_sync_changer_ces_noms_pourris()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        // TODO Rajouter int[]
        // TODO Rajouter List<int>
    }

    [Fact]
    public async Task Selector_and_async_changer_ces_noms_pourris_toto()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        // TODO Rajouter int[]
        // TODO Rajouter List<int>
    }

    [Fact]
    public async Task Selector_and_sync_changer_ces_noms_pourris_toto()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        // TODO Rajouter int[]
        // TODO Rajouter List<int>
    }
}

record Fake<T>(int Value, Task<T> List)
{
    public static IEnumerable<Fake<T>> Create(T list)
    {
        return Enumerable.Range(0, 10).Select(x => new Fake<T>(x, Task<T>.Factory.StartNew(() => list)));
    }

    public static IAsyncEnumerable<Fake<T>> CreateAsync(T list)
    {
        return AsyncLinq.RangeAsync(10).SelectAsync(x => new Fake<T>(x, Task<T>.Factory.StartNew(() => list)));
    }
}
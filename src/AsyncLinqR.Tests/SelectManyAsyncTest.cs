using System.Linq;

namespace AsyncLinqR.Tests;

public class SelectManyAsyncTest
{
    [Fact]
    public async Task SelectManyAsync_on_IEnumerable_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.Create([]).SelectManyAsync(x => x.List).ToListAsync();
        array.Should().BeOfType<List<int>>();
        
        var list = await Fake<List<int>>.Create([]).SelectManyAsync(x => x.List).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }

    [Fact]
    public async Task SelectManyAsync_on_IAsyncEnumerable_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.CreateAsync([]).SelectManyAsync(x => x.List).ToListAsync();
        array.Should().BeOfType<List<int>>();

        var list = await Fake<List<int>>.CreateAsync([]).SelectManyAsync(x => x.List).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }

    [Fact]
    public async Task SelectManyAsync_on_IEnumerable_with_selector_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.Create([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        array.Should().BeOfType<List<int>>();

        var list = await Fake<List<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }

    [Fact]
    public async Task SelectManyAsync_on_IAsyncEnumerable_with_selector_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        array.Should().BeOfType<List<int>>();

        var list = await Fake<List<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => i).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }

    [Fact]
    public async Task SelectManyAsync_on_IEnumerable_with_async_selector_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.Create([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        array.Should().BeOfType<List<int>>();

        var list = await Fake<List<int>>.Create([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }

    [Fact]
    public async Task SelectManyAsync_on_IAsyncEnumerable_with_async_selector_should_enable_type_inference_without_having_to_specify_generic_types()
    {
        var enumerable = await Fake<IEnumerable<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        enumerable.Should().BeOfType<List<int>>();

        var array = await Fake<int[]>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        array.Should().BeOfType<List<int>>();

        var list = await Fake<List<int>>.CreateAsync([]).SelectManyAsync(x => x.List, (y, i) => Task<int>.Factory.StartNew(() => i)).ToListAsync();
        list.Should().BeOfType<List<int>>();
    }
}

file record Fake<T>(int Value, Task<T> List)
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
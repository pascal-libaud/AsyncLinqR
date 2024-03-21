namespace AsyncLinqR.Tests;

public interface ISpy
{
    bool IsEnumerated { get; }
    bool IsEndReached { get; set; }
    int CountEnumeration { get; set; }
    int CountItemEnumerated { get; set; }
}

public interface ISpyEnumerable<out T> : ISpy, IEnumerable<T>;

public interface ISpyAsyncEnumerable<out T> : ISpy, IAsyncEnumerable<T>;
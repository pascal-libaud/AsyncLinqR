using System.Numerics;

namespace AsyncLinqR.Tests;

public static class RangeHelper
{
    public static IEnumerable<char> Alphabet()
    {
        for (char c = 'A'; c <= 'Z'; c++)
            yield return c;
    }

    public static IEnumerable<T> Range<T>(T count) where T : INumber<T>
    {
        for (T i = T.Zero; i < count; i++)
            yield return i;
    }
}
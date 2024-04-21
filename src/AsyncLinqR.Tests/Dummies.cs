namespace AsyncLinqR.Tests;

internal record Dummy(int Value);

abstract record DummyBase;

record Dummy1 : DummyBase;

record Dummy2 : DummyBase;

public record DummyIndexValue(int Index, int Value);
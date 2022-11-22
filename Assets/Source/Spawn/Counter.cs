using System;

public class Counter : ICounter
{
    public Counter(int total)
    {
        Total = total;
    }

    public int Total { get; private set; }
    public uint Value { get; private set; }

    public virtual void Increase()
    {
        if (Value >= Total)
            throw new InvalidOperationException();

        Value++;
    }
}
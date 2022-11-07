using System;

public class Counter
{
    private readonly int _total;

    public uint Value { get; private set; }

    public Counter(int total)
    {
        _total = total;
    }

    public event Action<uint> Changed;
    public event Action Complited;

    public void Increase()
    {
        Value++;
        Changed?.Invoke(Value);

        if (Value >= _total)
            Complited?.Invoke();
    }
}
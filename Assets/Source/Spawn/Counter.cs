using System;

public class Counter
{
    private readonly int _total;

    private int _value;

    public Counter(int total)
    {
        _total = total;
    }

    public event Action<int> Changed;
    public event Action Complited;

    public void Increase()
    {
        _value++;
        Changed?.Invoke(_value);

        if (_value >= _total)
            Complited?.Invoke();
    }
}
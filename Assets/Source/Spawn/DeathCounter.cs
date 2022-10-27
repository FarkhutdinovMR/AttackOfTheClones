using System;

public class DeathCounter
{
    private int _value;

    public event Action<int> Changed;

    public void Increase()
    {
        _value++;
        Changed?.Invoke(_value);
    }
}
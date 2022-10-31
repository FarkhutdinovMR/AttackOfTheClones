using System;

public class Health
{
    private readonly int _maxValue;
    private int _value;

    public Health(int maxValue)
    {
        _value = maxValue;
        _maxValue = maxValue;
    }

    public event Action<int, int> Changed;
    public event Action Died;

    public void TakeDamage(int value)
    {
        _value -= value;

        if (_value < 0)
            _value = 0;

        if (_value == 0)
            Die();

        Changed?.Invoke(_value, _maxValue);
    }

    public void Resurrect()
    {
        _value = _maxValue;
        Changed?.Invoke(_value, _maxValue);
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
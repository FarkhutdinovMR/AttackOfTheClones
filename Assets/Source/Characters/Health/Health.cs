using System;

public class Health : IHealth
{
    private readonly int _maxValue;
    private int _value;

    public Health(int maxValue)
    {
        _value = maxValue;
        _maxValue = maxValue;
    }

    public bool IsAlive => _value > 0;
    public float ValueInPercent => (float)_value / _maxValue;

    public virtual void TakeDamage(int value)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        _value -= value;

        if (_value < 0)
            _value = 0;

        if (_value == 0)
            Die();
    }

    public void Resurrect()
    {
        if (IsAlive)
            throw new InvalidOperationException();

        _value = _maxValue;
    }

    protected virtual void Die() { }
}
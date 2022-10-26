using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _value;
    private int _maxValue;

    public event Action<int, int> Changed;
    public event Action Died;

    public void Init(int maxValue)
    {
        _value = maxValue;
        _maxValue = maxValue;
    }

    public void TakeDamage(int value)
    {
        _value -= value;

        if (_value < 0)
            _value = 0;

        if (_value == 0)
            Die();

        Changed?.Invoke(_value, _maxValue);
    }

    private void Die()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
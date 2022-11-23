using System;
using UnityEngine;

[Serializable]
public class Level : ILevel
{
    private readonly uint _maxValue;

    public Level(uint startValue, uint maxValue)
    {
        _maxValue = maxValue;
        Value = startValue;
    }

    [field: SerializeField] public uint Value { get; private set; }
    public bool CanLevelUp => Value + 1 <= _maxValue;

    public virtual void LevelUp()
    {
        if (CanLevelUp == false)
            throw new InvalidOperationException();

        Value += 1;
    }
}
using System;
using UnityEngine;

[Serializable]
public class Level : ILevel
{
    public Level(uint startValue, uint maxValue)
    {
        if (startValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(startValue));

        MaxValue = maxValue;
        Value = startValue;
    }

    [field: SerializeField] public uint Value { get; private set; }
    [field: SerializeField] public uint MaxValue { get; private set; }
    public bool CanLevelUp => Value + 1 <= MaxValue;

    public virtual void LevelUp()
    {
        if (CanLevelUp == false)
            throw new InvalidOperationException();

        Value += 1;
    }
}
using System;
using UnityEngine;

[Serializable]
public class Experience : IExperience
{
    private readonly ILevel _level;
    private readonly float _progress;

    [field: SerializeField] public uint Value { get; private set; }
    [field: SerializeField] public uint LevelUpCost { get; private set; }

    public Experience(ILevel level, float progress, uint value, uint levelUpCost)
    {
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _progress = progress;
        Value = value;
        LevelUpCost = levelUpCost;
    }

    public void Add(uint value)
    {
        uint remain = value;
        while (remain > 0)
        {
            uint consumption = Math.Clamp(remain, 0, LevelUpCost - Value);
            Value += consumption;
            remain -= consumption;

            if (Value >= LevelUpCost && _level.CanLevelUp)
            {
                _level.LevelUp();
                Value = 0;
                LevelUpCost += (uint)(LevelUpCost * _progress);
            }
        }
    }
}
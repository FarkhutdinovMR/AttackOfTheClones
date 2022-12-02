using System;
using UnityEngine;

[Serializable]
public class NewState
{
    public NewState(float upgradeModificator, float baseValue, StateType type, Level level)
    {
        UpgradeModificator = upgradeModificator;
        BaseValue = baseValue;
        Type = type;
        Level = level ?? throw new ArgumentNullException(nameof(level));
    }

    [field: SerializeField] public StateType Type { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float UpgradeModificator { get; private set; }
    [field: SerializeField] public Level Level { get; private set; }

    public float Value => BaseValue + Level.Value * UpgradeModificator;
}
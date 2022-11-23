using System;
using UnityEngine;

[Serializable]
public class State
{
    public State(string name, StateType stateType, float baseValue, float maxLevel, float upgradeModificator, Sprite icon, uint level)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        StateType = stateType;
        BaseValue = baseValue;
        MaxLevel = maxLevel;
        UpgradeModificator = upgradeModificator;
        Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        Level = level;
    }

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public StateType StateType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float MaxLevel { get; private set; }
    [field: SerializeField] public float UpgradeModificator { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public uint Level { get; private set; } = 1;

    public float Value => BaseValue + Level * UpgradeModificator;

    public void Upgrade()
    {
        if (Level + 1 > MaxLevel)
            throw new InvalidOperationException();

        Level += 1;
    }
}

public enum StateType
{
    Damage,
    Radius,
    Interval
}
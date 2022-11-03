using System;
using UnityEngine;

[Serializable]
public class State
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public StateType Type { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public StateUpgrade Upgrade { get; private set; }

    public State(float baseValue, StateUpgrade stateUpgrade)
    {
        BaseValue = baseValue;
        Upgrade = stateUpgrade ?? throw new ArgumentNullException(nameof(stateUpgrade));
    }

    public float Value => BaseValue + Upgrade.Value;
}

[Serializable]
public class StateUpgrade
{
    [field: SerializeField] public uint Level { get; private set; }
    [field: SerializeField] public uint MaxLevel { get; private set; }
    [field: SerializeField] public float Modificator { get; private set; }

    public StateUpgrade(uint level, uint maxLevel, float modificator)
    {
        Level = level;
        MaxLevel = maxLevel;
        Modificator = modificator;
    }

    public float Value => (Level - 1) * Modificator;

    public void Upgrade()
    {
        if (Level + 1 > MaxLevel)
            throw new InvalidOperationException();

        Level += 1;
    }
}

public enum StateType
{
    AttackDamage,
    AttackRadius,
    AttackInterval
}
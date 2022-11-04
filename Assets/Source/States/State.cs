using System;
using UnityEngine;

[Serializable]
public class State
{
    [field: SerializeField] public StateConfig Config { get; private set; }

    public uint Level { get; private set; } = 1;
    public float Value => Config.BaseValue + Level * Config.UpgradeModificator;

    public void Upgrade()
    {
        if (Level + 1 > Config.MaxLevel)
            throw new InvalidOperationException();

        Level += 1;
    }
}
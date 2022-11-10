using System;
using UnityEngine;

[Serializable]
public class State
{
    public State(StateConfig config)
    {
        Config = config ?? throw new ArgumentNullException(nameof(config));
        Level = 1;
    }

    [field: SerializeField] public uint Level { get; private set; }
    [field: SerializeField] public StateConfig Config { get; private set; }
    public float Value => Config.BaseValue + Level * Config.UpgradeModificator;

    public void Upgrade()
    {
        if (Level + 1 > Config.MaxLevel)
            throw new InvalidOperationException();

        Level += 1;
    }
}
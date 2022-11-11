using System;
using UnityEngine;

[Serializable]
public class AbilityData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public uint Cost { get; private set; }
    [field: SerializeField] public Ability Template { get; private set; }
    [field: SerializeField] public bool IsBought { get; private set; }
    [field: SerializeField] public State[] States { get; private set; }

    public void Buy()
    {
        if (IsBought)
            throw new InvalidOperationException();

        IsBought = true;
    }
}
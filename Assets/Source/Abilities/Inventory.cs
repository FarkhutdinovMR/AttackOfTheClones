using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    [field: SerializeField] public AbilityData[] Abilities { get; private set; }
    [field: SerializeField] public Slot[] Slots { get; private set; }

    public bool ContainInSlot(AbilityData ability)
    {
        return Array.Exists(Slots, slot => slot.Ability != null && slot.Ability.Name == ability.Name);
    }
}
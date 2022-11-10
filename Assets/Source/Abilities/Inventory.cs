using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeReference] private List<AbilitySlot> _slots;
    [SerializeReference] private List<Ability> _abilities;

    public IEnumerable<AbilitySlot> Slots => _slots;
    public IEnumerable<Ability> Abilities => _abilities;

    public Inventory(List<AbilitySlot> slots)
    {
        _slots = slots;
        _abilities = new();
    }

    public bool ContainInSlots(Type abilityType)
    {
        if (abilityType == null)
            throw new ArgumentNullException(nameof(abilityType));

        AbilitySlot result = _slots.Find(slot => slot.Ability != null && slot.Ability.GetType() == abilityType);
        return result != null;
    }

    public bool Contain(Type abilityType)
    {
        if (abilityType == null)
            throw new ArgumentNullException(nameof(abilityType));

        Ability result = _abilities.Find(ability => ability.GetType() == abilityType);
        return result != null;
    }

    public void AddAbility(Ability ability)
    {
        if (ability == null)
            throw new ArgumentNullException(nameof(ability));

        if (_abilities.Contains(ability))
            throw new InvalidOperationException();

        _abilities.Add(ability);
    }

    public void Equip(Ability ability, int slotIndex)
    {
        if (ability == null)
            throw new ArgumentNullException(nameof(ability));

        if (slotIndex >= _slots.Count)
            throw new ArgumentOutOfRangeException(nameof(slotIndex));

        _slots[slotIndex].Equip(ability);
    }

    public Ability FindAbility(Type abilityType)
    {
        if (abilityType == null)
            throw new ArgumentNullException(nameof(abilityType));

        return _abilities.Find(ability => ability.GetType() == abilityType);
    }
}
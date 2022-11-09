using System;
using System.Collections.Generic;

public class AbilityInventory
{
    public readonly List<Slot> Slots;

    public AbilityInventory(List<Slot> slots)
    {
        Slots = slots;
    }

    public bool Contain(Ability ability)
    {
        if (ability == null)
            throw new ArgumentNullException(nameof(ability));

        Slot result = Slots.Find(slot => slot.Ability != null && slot.Ability == ability);
        return result != null;
    }
}
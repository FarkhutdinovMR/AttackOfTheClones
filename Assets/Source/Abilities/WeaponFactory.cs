using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private GetNearbyBot _targetSource;

    private List<Weapon> _weapons = new();

    public void CreateWeapons()
    {
        if (_weapons.Count > 0)
            Clear();

        foreach (AbilitySlot slot in _character.Inventory.Slots)
        {
            if (slot.IsEmpty)
                continue;

            Weapon weapon = Instantiate(slot.Ability.Weapon, transform);
            weapon.Init(slot, _targetSource);
            slot.AddStates(_character.States);
            _weapons.Add(weapon);
            weapon.Use();
        }
    }

    private void Clear()
    {
        foreach (Weapon ability in _weapons)
            Destroy(ability.gameObject);

        _weapons.Clear();
    }
}
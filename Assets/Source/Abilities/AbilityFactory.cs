using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private Ability[] _abilitiesTemplate;
    [SerializeField] private Character _character;
    [SerializeField] private GetNearbyBot _targetSource;

    private List<Ability> _abilities = new();
    private IEnumerable<Slot> _slots;
    private Saver.Data _data;

    public Slot[] Slots { get; private set; }
    public IEnumerable<Ability> Abilities => _abilities;

    public void Init(IEnumerable<Slot> slots, Saver.Data data)
    {
        _slots = slots;
        _data = data;
    }

    public void UpdateSlots()
    {
        if (_abilities.Count > 0)
            Clear();

        foreach (Slot slot in _slots)
        {
            if (slot.IsEmpty)
                continue;

            slot.Clear();
            Ability newAbility = Instantiate(slot.Ability, transform);
            newAbility.Init(slot, _targetSource, _data);
            slot.AddStates(newAbility.States);
            slot.AddStates(_character.States);
            _abilities.Add(newAbility);
        }

        Use();
    }

    private void Clear()
    {
        foreach (Ability ability in _abilities)
            Destroy(ability.gameObject);

        _abilities.Clear();
    }

    private void Use()
    {
        foreach (IAbility ability in _abilities)
            ability.Use();
    }
}
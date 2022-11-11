using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private GetNearbyBot _targetSource;

    public void Create()
    {
        foreach (Slot slot in _character.Inventory.Slots)
        {
            if (slot.IsEmpty)
                continue;

            Ability newAbility = Instantiate(slot.Ability.Template, transform);
            newAbility.Init(slot, _targetSource);
            slot.AddStates(_character.States);
            slot.AddStates(slot.Ability.States);
            newAbility.Use();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInventoryView : MonoBehaviour
{
    [SerializeField] private AbilitySlotView _abilityViewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private StoreView _storeView;
    [SerializeField] private Character _character;

    private List<AbilitySlotView> _abilityViews = new();

    public void Render()
    {
        if (_abilityViews.Count > 0)
            Clear();

        foreach (AbilitySlot slot in _character.Inventory.Slots)
        {
            AbilitySlotView newAbilityView = Instantiate(_abilityViewTemplate, _container);
            newAbilityView.Init(slot, _character, _storeView.SelectedProduct, OnSlotSelected);
            newAbilityView.Render();
            _abilityViews.Add(newAbilityView);
        }
    }

    private void Clear()
    {
        foreach (AbilitySlotView view in _abilityViews)
            Destroy(view.gameObject);

        _abilityViews.Clear();
    }

    private void OnSlotSelected(AbilitySlot slot)
    {
        Type abilityType = Type.GetType(_storeView.SelectedProduct.Name);
        if (_storeView.SelectedProduct == null || _character.Inventory.Contain(abilityType) == false || _character.Inventory.ContainInSlots(abilityType))
            return;

        slot.Equip(_character.Inventory.FindAbility(abilityType));
        Render();
        _storeView.Render();
    }
}
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

        foreach (Slot slot in _character.Inventory.Slots)
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

    private void OnSlotSelected(Slot slot)
    {
        if (_storeView.SelectedProduct == null || _storeView.SelectedProduct.IsBought == false || _character.Inventory.ContainInSlot(_storeView.SelectedProduct))
            return;

        slot.Equip(_storeView.SelectedProduct);
        Render();
        _storeView.Render();
    }
}
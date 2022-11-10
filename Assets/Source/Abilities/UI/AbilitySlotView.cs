using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _lock;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private GameObject _activeSlotIcon;

    private AbilitySlot _slot;
    private Character _character;
    private Product _product;
    private Action<AbilitySlot> _onSlotSelect;

    private const string Lvl = "lvl ";
    private bool _isLock => _character.Level.Value < _slot.UnlockLevel;

    public void Init(AbilitySlot slot, Character character, Product product, Action<AbilitySlot> onSlotSelect)
    {
        _slot = slot;
        _character = character;
        _product = product;
        _onSlotSelect = onSlotSelect;
    }

    public void Render()
    {
        _lock.SetActive(_isLock);
        _lvl.SetText(Lvl + _slot.UnlockLevel.ToString());

        if (_isLock)
            return;

        if (_product != null && _character.Inventory.Contain(Type.GetType(_product.Name)) && _character.Inventory.ContainInSlots(Type.GetType(_product.Name)) == false)
            _activeSlotIcon.SetActive(true);

        if (_slot.IsEmpty)
            return;

        _icon.sprite = _slot.Ability.Icon;
        _icon.gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (_isLock)
            return;

        _onSlotSelect?.Invoke(_slot);
    }
}
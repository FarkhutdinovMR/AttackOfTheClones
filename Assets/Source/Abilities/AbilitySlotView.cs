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

    private Slot _slot;
    private Character _character;
    private AbilityProduct _product;
    private Action<Slot> _onSlotSelect;

    private const string Lvl = "lvl ";
    private bool _isLock => _character.Level.Value < _slot.UnlockLevel;

    public void Init(Slot slot, Character character, AbilityProduct product, Action<Slot> onSlotSelect)
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

        if (_product != null && _product.IsBought && _character.Inventory.Contain(_product.Info.Ability) == false)
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
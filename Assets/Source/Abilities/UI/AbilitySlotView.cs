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
    private AbilityData _ability;
    private Action<Slot> _onSlotSelect;

    private bool _isLock => _character.Level.Value < _slot.UnlockLevel;

    public void Init(Slot slot, Character character, AbilityData ability, Action<Slot> onSlotSelect)
    {
        _slot = slot;
        _character = character;
        _ability = ability;
        _onSlotSelect = onSlotSelect;
    }

    public void Render()
    {
        _lock.SetActive(_isLock);
        _lvl.SetText(_slot.UnlockLevel.ToString());

        if (_isLock)
            return;

        if (_ability != null && _ability.IsBought && _character.Inventory.ContainInSlot(_ability) == false)
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
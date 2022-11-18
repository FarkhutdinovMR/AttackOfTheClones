using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _isBoughtIcon;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _activeAbilitySlot;

    private AbilityData _ability;
    private Inventory _inventory;
    private Action<AbilityData> _onSelectProductCallback;

    public void Init(AbilityData ability, Inventory inventory, Action<AbilityData> onSelectProductCallback)
    {
        _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _onSelectProductCallback = onSelectProductCallback;
    }

    public void Render()
    {
        _icon.sprite = _ability.Icon;

        if (_ability.IsBought)
        {
            _isBoughtIcon.SetActive(true);

            if (_inventory.ContainInSlot(_ability))
                _background.sprite = _activeAbilitySlot;
        }
    }

    public void OnClickButton()
    {
        _onSelectProductCallback?.Invoke(_ability);
    }
}
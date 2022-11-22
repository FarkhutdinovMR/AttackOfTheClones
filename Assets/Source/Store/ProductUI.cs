using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _isBoughtIcon;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _activeAbilitySlot;

    private AbilityData _ability;
    private bool _containInSlot;
    private Action<AbilityData> _onSelectProductCallback;

    public void Init(AbilityData ability, bool containInSlot, Action<AbilityData> onSelectProductCallback)
    {
        _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        _containInSlot = containInSlot;
        _onSelectProductCallback = onSelectProductCallback;
    }

    public void Render()
    {
        _icon.sprite = _ability.Icon;

        if (_ability.IsBought)
        {
            _isBoughtIcon.SetActive(true);

            if (_containInSlot)
                _background.sprite = _activeAbilitySlot;
        }
    }

    public void OnButtonClick()
    {
        _onSelectProductCallback?.Invoke(_ability);
    }
}
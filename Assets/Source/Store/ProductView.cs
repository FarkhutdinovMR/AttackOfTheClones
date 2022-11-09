using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _isBoughtIcon;
    [SerializeField] private Image _background;
    [SerializeField] private Color _activeColor;

    private AbilityProduct _product;
    private AbilityInventory _inventory;
    private Action<AbilityProduct> _onSelectProductCallback;

    public void Init(AbilityProduct product, AbilityInventory inventory, Action<AbilityProduct> onSelectProductCallback)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _onSelectProductCallback = onSelectProductCallback;
    }

    public void Render()
    {
        _icon.sprite = _product.Info.Icon;
        _isBoughtIcon.SetActive(_product.IsBought);

        if (_inventory.Contain(_product.Info.Ability))
            _background.color = _activeColor;
    }

    public void OnClickButton()
    {
        _onSelectProductCallback?.Invoke(_product);
    }
}
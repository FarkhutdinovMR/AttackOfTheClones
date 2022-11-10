using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _isBoughtIcon;
    [SerializeField] private Image _background;
    [SerializeField] private Color _activeColor;

    private Product _product;
    private Inventory _inventory;
    private Action<Product> _onSelectProductCallback;

    public void Init(Product product, Inventory inventory, Action<Product> onSelectProductCallback)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _onSelectProductCallback = onSelectProductCallback;
    }

    public void Render()
    {
        _icon.sprite = _product.Icon;

        if (_inventory.Contain(Type.GetType(_product.Name)))
        {
            _isBoughtIcon.SetActive(true);

            if (_inventory.ContainInSlots(Type.GetType(_product.Name)))
                _background.color = _activeColor;
        }
    }

    public void OnClickButton()
    {
        _onSelectProductCallback?.Invoke(_product);
    }
}
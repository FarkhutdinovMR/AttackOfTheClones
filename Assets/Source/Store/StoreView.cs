using System.Collections.Generic;
using UnityEngine;

public class StoreView : MonoBehaviour, IStoreView
{
    [SerializeField] private Store _store;
    [SerializeField] private ProductView _productViewTemplate;
    [SerializeField] private BuyButtonView _buyButton;
    [SerializeField] private Transform _container;
    [SerializeField] private ProductInfoView _productInfo;
    [SerializeField] private TextView _gold;
    [SerializeField] private Character _character;
    [SerializeField] private AbilityInventoryView _abilityInventoryView;

    private List<ProductView> _productViews = new();

    public AbilityProduct SelectedProduct { get; private set; }

    public void Render()
    {
        if (_productViews.Count > 0)
            Clear();

        foreach (AbilityProduct product in _store.Products)
        {
            ProductView newProductView = Instantiate(_productViewTemplate, _container);
            newProductView.Init(product, _character.Inventory, OnProductSelected);
            newProductView.Render();
            _productViews.Add(newProductView);
        }

        _gold.Render(_store.Wallet.Gold);

        if (SelectedProduct != null)
        {
            _productInfo.Render(SelectedProduct);
            _buyButton.Render(SelectedProduct);
        }
    }

    public void OnStoreButtonClick()
    {
        gameObject.SetActive(true);
        Render();
        _abilityInventoryView.Render();
    }

    public void OnCloseWindowButtonClick()
    {
        gameObject.SetActive(false);
        _store.Close();
    }

    private void Clear()
    {
        foreach (ProductView view in _productViews)
            Destroy(view.gameObject);

        _productViews.Clear();
    }

    private void OnProductSelected(AbilityProduct product)
    {
        SelectedProduct = product;
        _productInfo.Render(SelectedProduct);
        _abilityInventoryView.Render();
        _buyButton.Render(SelectedProduct);
    }

    public void OnBuyButtonClick()
    {
        _store.Buy(SelectedProduct);
    }
}
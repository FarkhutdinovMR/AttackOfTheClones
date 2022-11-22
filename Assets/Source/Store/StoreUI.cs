using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreUI : MonoBehaviour, IStoreUI
{
    [SerializeField] private ProductUI _productUITemplate;
    [SerializeField] private BuyButtonView _buyButton;
    [SerializeField] private Transform _container;
    [SerializeField] private ProductInfoView _productInfo;
    [SerializeField] private SlotUI _slotsUI;
    [SerializeField] private MonoBehaviour _walletViewSource;
    public IWalletView _walletView => (IWalletView)_walletViewSource;

    private IStore _store;
    private Character _character;

    private List<ProductUI> _productsUI = new();
    public AbilityData SelectedProduct { get; private set; }

    private void OnValidate()
    {
        if (_walletViewSource && !(_walletViewSource is IWalletView))
        {
            Debug.LogError(_walletViewSource + " not implement " + nameof(IWalletView));
            _walletViewSource = null;
        }
    }

    public void Init(IStore store, Character character)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _character = character;
    }

    public void Render()
    {
        CreateAssortment();
        _character.Wallet.ShowGold(_walletView);
        RenderSelectedProduct();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        SelectedProduct = _store.Products.First();
        Render();
        _slotsUI.Render();
        _character.Input.Disable();
    }

    public void OnCloseWindowButtonClick()
    {
        gameObject.SetActive(false);
        _character.Input.Enable();
    }

    public void OnBuyButtonClick()
    {
        _store.Buy(SelectedProduct);
        _slotsUI.Render();
    }

    private void CreateAssortment()
    {
        if (_productsUI.Count > 0)
            Clear();

        foreach (AbilityData product in _store.Products)
        {
            ProductUI newProductView = Instantiate(_productUITemplate, _container);
            newProductView.Init(product, _character.Inventory.ContainInSlot(product), OnProductSelected);
            newProductView.Render();
            _productsUI.Add(newProductView);
        }
    }

    private void Clear()
    {
        foreach (ProductUI view in _productsUI)
            Destroy(view.gameObject);

        _productsUI.Clear();
    }

    private void RenderSelectedProduct()
    {
        if (SelectedProduct != null)
        {
            _productInfo.Render(SelectedProduct);
            _buyButton.Render(SelectedProduct);
        }
    }

    private void OnProductSelected(AbilityData ability)
    {
        SelectedProduct = ability;
        _productInfo.Render(SelectedProduct);
        _slotsUI.Render();
        _buyButton.Render(SelectedProduct);
    }
}
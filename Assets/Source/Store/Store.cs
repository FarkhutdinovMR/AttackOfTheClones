using System;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private Product[] _products;
    [SerializeField] private MonoBehaviour _storeViewSource;
    [SerializeField] private Saver _saver;
    [SerializeField] private Character _character;
    [SerializeField] private AbilityFactory _abilityFactory;

    private IStoreView _storeView => (IStoreView)_storeViewSource;
    public IEnumerable<Product> Products => _products;

    private void OnValidate()
    {
        if (_storeViewSource && !(_storeViewSource is IStoreView))
        {
            Debug.LogError(_storeViewSource + " not implement " + nameof(IStoreView));
            _storeViewSource = null;
        }
    }

    public void Buy(Product product)
    {
        if (_character.Wallet.TryBuy(product.Cost) == false)
            return;

        _character.Inventory.AddAbility(_abilityFactory.CreateAbility(Type.GetType(product.Name)));
        _storeView.Render();
    }

    public void Close()
    {
        _saver.Save();
    }
}
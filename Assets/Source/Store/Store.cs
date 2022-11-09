using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private AbilityProductInfo[] _productInfo;
    [SerializeField] private MonoBehaviour _storeViewSource;
    [SerializeField] private Saver _saver;
    [SerializeField] private Character _character;

    private List<AbilityProduct> _products;
    public Wallet Wallet { get; private set; }

    private IStoreView _storeView => (IStoreView)_storeViewSource;
    public IEnumerable<AbilityProduct> Products => _products;

    private void OnValidate()
    {
        if (_storeViewSource && !(_storeViewSource is IStoreView))
        {
            Debug.LogError(_storeViewSource + " not implement " + nameof(IStoreView));
            _storeViewSource = null;
        }
    }

    public void Init(Wallet wallet)
    {
        Wallet = wallet;
        _products = _saver.PlayerData.AbilityProducts;
    }

    public void Buy(AbilityProduct product)
    {
        if (Wallet.TryBuy(product.Info.Cost) == false)
            return;

        product.Buy();
        _storeView.Render();
    }

    public void Close()
    {
        _saver.SaveSlots(_character.Inventory.Slots);
        _saver.Save();
    }
}
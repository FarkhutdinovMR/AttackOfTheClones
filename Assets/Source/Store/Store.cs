using System;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _storeViewSource;
    [SerializeField] private Saver _saver;
    [SerializeField] private Character _character;

    private IStoreView _storeView => (IStoreView)_storeViewSource;

    private void OnValidate()
    {
        if (_storeViewSource && !(_storeViewSource is IStoreView))
        {
            Debug.LogError(_storeViewSource + " not implement " + nameof(IStoreView));
            _storeViewSource = null;
        }
    }

    public void Buy(AbilityData ability)
    {
        if (_character.Wallet.TryBuy(ability.Cost) == false)
            return;

        ability.Buy();
        _storeView.Render();
    }

    public void Close()
    {
        _saver.Save();
    }
}
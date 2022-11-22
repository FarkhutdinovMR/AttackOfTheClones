using System;
using UnityEngine;

[Serializable]
public class Wallet : IWallet
{
    public Wallet(uint gold)
    {
        Gold = gold;
    }

    [field: SerializeField] public uint Gold { get; private set; }

    public void Add(uint gold)
    {
        Gold += gold;
    }

    public bool TryBuy(uint cost)
    {
        if (Gold < cost)
            return false;

        Gold -= cost;
        return true;
    }

    public void ShowGold(IWalletView walletView)
    {
        walletView.Render(Gold);
    }
}
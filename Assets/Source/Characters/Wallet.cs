using System;
using UnityEngine;

[Serializable]
public class Wallet
{
    public Wallet(uint gold)
    {
        Gold = gold;
    }

    [field: SerializeField] public uint Gold { get; private set; }

    public event Action<uint> Changed;

    public void Add(uint gold)
    {
        Gold += gold;
        Changed?.Invoke(Gold);
    }

    public bool TryBuy(uint cost)
    {
        if (Gold < cost)
            return false;

        Gold -= cost;
        Changed?.Invoke(Gold);
        return true;
    }
}
using System;
using UnityEngine;

[Serializable]
public class Product
{
    public Product(bool isBought, ProductInfo info)
    {
        IsBought = isBought;
        Info = info ?? throw new ArgumentNullException(nameof(info));
    }

    [field: SerializeField] public bool IsBought { get; private set; }
    [field: SerializeField] public ProductInfo Info { get; private set; }

    public void Buy()
    {
        if (IsBought)
            throw new InvalidOperationException();

        IsBought = true;
    }
}
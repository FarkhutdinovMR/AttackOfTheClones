using System;

public class Product
{
    public Product(bool isBought, ProductInfo info)
    {
        IsBought = isBought;
        Info = info ?? throw new ArgumentNullException(nameof(info));
    }

    public bool IsBought { get; private set; }
    public ProductInfo Info { get; private set; }

    public void Buy()
    {
        if (IsBought)
            throw new InvalidOperationException();

        IsBought = true;
    }
}
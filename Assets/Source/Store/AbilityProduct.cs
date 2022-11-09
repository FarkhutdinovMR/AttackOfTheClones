using System;

[Serializable]
public class AbilityProduct : Product
{
    new public AbilityProductInfo Info;

    public AbilityProduct(bool isBought, AbilityProductInfo info) : base(isBought, info)
    {
        Info = info;
    }
}
using System;
using System.Collections.Generic;

public class Store : IStore
{
    private readonly Inventory _inventory;
    private readonly IWallet _wallet;

    public Store(Inventory inventory, IWallet wallet)
    {
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
    }

    public IEnumerable<AbilityData> Products => _inventory.Abilities;

    public void Buy(AbilityData ability)
    {
        if (_wallet.TryBuy(ability.Cost) == false)
            return;

        ability.Buy();
    }
}

public interface IStore
{
    void Buy(AbilityData ability);
    IEnumerable<AbilityData> Products { get; }
}
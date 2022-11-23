public interface IWallet
{
    void Add(uint gold);
    bool TryBuy(uint cost);
    void ShowGold(IWalletView walletView);
}

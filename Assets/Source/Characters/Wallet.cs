public class Wallet
{
    public uint Gold { get; private set; }

    public void Add(uint gold)
    {
        Gold += gold;
    }
}
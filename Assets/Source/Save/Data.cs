using System;

[Serializable]
public class Data
{
    public Wallet Wallet;
    public State[] CharacterState;
    public Inventory Inventory;
    public CharacterScore Score;
    public Experience Experience;
    public CharacterLevel Level;
    public int NextLevel;
    public bool IsMute;
}
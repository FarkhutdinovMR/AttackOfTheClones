using System;

public class CharacterLevel
{
    public uint Exp { get; private set; }
    public uint Value { get; private set; }
    public uint LevelUpCost { get; private set; }

    public CharacterLevel(uint exp, uint value, uint levelUp)
    {
        Exp = exp;
        Value = value;
        LevelUpCost = levelUp;
    }

    public CharacterLevel(CharacterLevel characterLevel)
    {
        Exp = characterLevel.Exp;
        Value = characterLevel.Value;
        LevelUpCost = characterLevel.LevelUpCost;
    }

    public event Action<uint> LevelChanged;

    public void AddExp(uint value)
    {
        uint remain = value;

        while (remain > 0)
        {
            Exp++;
            remain--;

            if (Exp >= LevelUpCost)
            {
                Value++;
                LevelUpCost += LevelUpCost;
                LevelChanged?.Invoke(Value);
            }
        }
    }
}
using System;
using UnityEngine;

[Serializable]
public class CharacterLevel
{
    private readonly float _progress;
    [SerializeField] private uint _levelUpCost;

    public CharacterLevel(uint exp, uint value, uint levelUp, float progress)
    {
        Exp = exp;
        _levelUpCost = levelUp;
        Value = value;
        _progress = progress;
    }

    [SerializeField] public uint Value { get; private set; }
    [SerializeField] public uint Exp { get; private set; }

    public event Action<uint> LevelChanged;

    public void AddExp(uint value)
    {
        uint remain = value;

        while (remain > 0)
        {
            Exp++;
            remain--;

            if (Exp >= _levelUpCost)
            {
                Value++;
                _levelUpCost += (uint)(_levelUpCost * _progress);
                Exp = 0;
                LevelChanged?.Invoke(Value);
            }
        }
    }
}
using System;
using UnityEngine;

[Serializable]
public class CharacterLevel
{
    [SerializeField] private float _progress;
    [SerializeField] private uint _levelUpCost;

    public CharacterLevel(uint value, uint exp, uint score, uint levelUp, float progress)
    {
        Value = value;
        Exp = exp;
        Score = score;
        _levelUpCost = levelUp;
        _progress = progress;
    }

    [field: SerializeField] public uint Value { get; private set; }
    [field: SerializeField] public uint Score { get; private set; }
    [field: SerializeField] public uint Exp { get; private set; }

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

        Score += value;
    }
}
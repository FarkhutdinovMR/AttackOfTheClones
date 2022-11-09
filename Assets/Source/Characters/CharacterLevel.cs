using System;
using UnityEngine;

[Serializable]
public class CharacterLevel
{
    [SerializeField] private uint _exp;
    [SerializeField] private uint _levelUpCost;

    public CharacterLevel(uint exp, uint value, uint levelUp)
    {
        _exp = exp;
        _levelUpCost = levelUp;
        Value = value;
    }

    [field: SerializeField] public uint Value { get; private set; }

    public event Action<uint> LevelChanged;

    public void AddExp(uint value)
    {
        uint remain = value;

        while (remain > 0)
        {
            _exp++;
            remain--;

            if (_exp >= _levelUpCost)
            {
                Value++;
                _levelUpCost += _levelUpCost;
                LevelChanged?.Invoke(Value);
            }
        }
    }
}
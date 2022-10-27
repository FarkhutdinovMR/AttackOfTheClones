using System;
using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    [SerializeField] private uint _startLevelUp = 10;

    private uint _value = 1;
    private uint _exp;
    private uint _levelUp;

    public event Action<uint> LevelChanged;

    private void Awake()
    {
        _levelUp = _startLevelUp;
    }

    public void Init(uint exp, uint value, uint levelUp)
    {
        _exp = exp;
        _value = value;
        _levelUp = levelUp;
    }

    public void AddExp(uint value)
    {
        uint remain = value;

        while (remain > 0)
        {
            _exp++;
            remain--;

            if (_exp >= _levelUp)
            {
                _value++;
                _levelUp += _levelUp;
                LevelChanged?.Invoke(_value);
            }
        }
    }
}
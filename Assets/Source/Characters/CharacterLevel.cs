using System;

public class CharacterLevel
{
    private readonly uint _startLevelUp;

    private uint _levelUp;
    private uint _exp;
    private uint _value;

    public CharacterLevel(uint exp, uint value, uint levelUp)
    {
        _levelUp = _startLevelUp;
        _exp = exp;
        _value = value;
        _levelUp = levelUp;
    }

    public event Action<uint> LevelChanged;

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
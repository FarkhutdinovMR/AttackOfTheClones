using System;
using UnityEngine;

[Serializable]
public class CharacterLevel
{
    [SerializeField] private float _progress;
    [SerializeField] private uint _levelUpCost;

    private IGame _game;

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

    public void AddExp(uint value)
    {
        uint remain = value;
        while (remain > 0)
        {
            Exp++;
            remain--;

            if (Exp >= _levelUpCost)
                LevelUp();
        }

        Score += value;
    }

    public void SetGame(IGame game)
    {
        _game = game;
    }

    private void LevelUp()
    {
        Value++;
        _levelUpCost += (uint)(_levelUpCost * _progress);
        Exp = 0;
        _game.UpgradeCharacter();
    }
}
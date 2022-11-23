using System;

[Serializable]
public class CharacterLevel : Level
{
    private readonly IGame _game;

    public CharacterLevel(uint startValue, uint maxValue, IGame game) : base(startValue, maxValue)
    {
        _game = game ?? throw new ArgumentNullException(nameof(game));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        _game.UpgradeCharacter();
    }
}
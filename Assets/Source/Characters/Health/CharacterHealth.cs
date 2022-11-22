using System;

public class CharacterHealth : Health
{
    private readonly IHealthView _healthView;
    private readonly IGame _game;

    public CharacterHealth(int maxValue, IHealthView healthView, IGame levelCompositeRoot) : base(maxValue)
    {
        _healthView = healthView ?? throw new ArgumentNullException(nameof(healthView));
        _game = levelCompositeRoot ?? throw new ArgumentNullException(nameof(levelCompositeRoot));
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        _healthView.Render(ValueInPercent);
    }

    protected override void Die()
    {
        _game.GameOver();
    }
}
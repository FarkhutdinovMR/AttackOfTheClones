using System;

public class EnemiesDeathCounter : Counter
{
    private readonly ICounterView _counterView;
    private readonly IGame _game;

    public EnemiesDeathCounter(int total, ICounterView counterView, IGame game) : base(total)
    {
        _counterView = counterView ?? throw new ArgumentNullException(nameof(counterView));
        _game = game ?? throw new ArgumentNullException(nameof(game));
    }

    public override void Increase()
    {
        base.Increase();
        _counterView.Render((float)Value / Total);

        if (Value >= Total)
            _game.CompleteLevel();
    }
}
using System;
using UnityEngine;

public class BotHealth : Health
{
    private readonly DamageVisualizer _damageVisualizer;
    private readonly RewardSpawner _rewardSpawner;
    private readonly GameObject _gameObject;
    private readonly ICounter _deathCounter;

    public BotHealth(int maxValue, DamageVisualizer damageVisualizer, RewardSpawner rewardSpawner, GameObject gameObject, ICounter deathCounter) : base(maxValue)
    {
        _damageVisualizer = damageVisualizer ?? throw new ArgumentNullException(nameof(damageVisualizer));
        _rewardSpawner = rewardSpawner ?? throw new ArgumentNullException(nameof(rewardSpawner));
        _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        _deathCounter = deathCounter ?? throw new ArgumentNullException(nameof(deathCounter));
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        if (IsAlive)
            _damageVisualizer.Render();
    }

    protected override void Die()
    {
        _rewardSpawner.Spawn(_gameObject.transform.position, _gameObject.transform.rotation);
        _deathCounter.Increase();
        _gameObject.SetActive(false);
    }
}
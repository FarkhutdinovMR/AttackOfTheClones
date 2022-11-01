using System;
using UnityEngine;

public class BotSpawner : ISpawner
{
    private readonly BotObjectPool _pool;
    private readonly Character _character;  
    private readonly Counter _deathCounter;
    private readonly RewardObjectPool _rewardObjectPool;

    public BotSpawner(BotObjectPool pool, Character character, Counter deathCounter, RewardObjectPool rewardObjectPool)
    {
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        _character = character ?? throw new ArgumentNullException(nameof(character));
        _deathCounter = deathCounter ?? throw new ArgumentNullException(nameof(deathCounter));
        _rewardObjectPool = rewardObjectPool ?? throw new ArgumentNullException(nameof(rewardObjectPool));
    }

    public bool Spawn(Vector3 position)
    {
        Bot bot = _pool.GetPooledObject();
        if (bot == null)
            return false;

        bot.transform.SetPositionAndRotation(position, Quaternion.identity);
        bot.gameObject.SetActive(true);

        if (bot.Health != null)
            bot.Health.Resurrect();
        else
            bot.Init(_character, _deathCounter, _rewardObjectPool);

        return true;
    }
}
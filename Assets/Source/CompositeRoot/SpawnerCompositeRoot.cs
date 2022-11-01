using UnityEngine;

public class SpawnerCompositeRoot : CompositeRoot
{
    [SerializeField] private Config _config;
    [SerializeField] private Bot _template;
    [SerializeField] private Character _character;
    [SerializeField] private LevelCompositeRoot _level;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private BotObjectPool _botObjectPool;
    [SerializeField] private RewardObjectPool _rewardObjectPool;

    public override void Compose()
    {
        var botSpawner = new BotSpawner(_botObjectPool, _character, _level.DeathCounter, _rewardObjectPool);
        var waveInCircle = new SpawnWaveInCircle(_config.WaveSpawnerStartRadius, _config.WaveSpawnerDistanceBetweenCircles, _config.WaveSpawnerStartAngleStep, botSpawner);
        _waveSpawner.Init(waveInCircle);
    }
}
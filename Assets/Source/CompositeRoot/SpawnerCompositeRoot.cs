using UnityEngine;

public class SpawnerCompositeRoot : CompositeRoot
{
    [SerializeField] private Config _config;
    [SerializeField] private Bot _template;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _container;
    [SerializeField] private LevelCompositeRoot _level;
    [SerializeField] private WaveSpawner _waveSpawner;

    public override void Compose()
    {
        var botSpawner = new BotSpawner(_template, _character, _container, _level.DeathCounter);
        var waveInCircle = new SpawnWaveInCircle(_config.WaveSpawnerStartRadius, _config.WaveSpawnerDistanceBetweenCircles, _config.WaveSpawnerStartAngleStep, botSpawner);
        _waveSpawner.Init(waveInCircle);
    }
}
using UnityEngine;

namespace CompositeRoot
{
    public class EnemiesCompositeRoot : CompositeRoot
    {
        [SerializeField] private Config _config;
        [SerializeField] private Bot _template;
        [SerializeField] private CharacterCompositeRoot _characterCompositeRoot;
        [SerializeField] private LevelCompositeRoot _level;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private BotObjectPool _botObjectPool;
        [SerializeField] private RewardObjectPool _rewardObjectPool;
        [SerializeField] private SceneLoader _sceneLoader;

        public override void Compose()
        {
            var botSpawner = new BotSpawner(_botObjectPool, _characterCompositeRoot.Character, _level.DeathCounter, _rewardObjectPool);
            var waveInCircle = new SpawnWaveInCircle(_config.WaveSpawnerStartRadius, _config.WaveSpawnerDistanceBetweenCircles, _config.WaveSpawnerStartAngleStep, botSpawner);
            _waveSpawner.Init(waveInCircle, _level.EnemyAmount, (int)_config.WaveSpawnerAmountInOnWave.Evaluate(_sceneLoader.CurrentSceneIndex));
        }
    }
}
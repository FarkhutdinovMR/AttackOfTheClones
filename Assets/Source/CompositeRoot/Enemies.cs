using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private Bot _template;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private BotObjectPool _botObjectPool;
    [SerializeField] private RewardObjectPool _rewardObjectPool;
    [SerializeField] private MonoBehaviour _levelProgressViewSource;
    private ICounterView _levelProgressView => (ICounterView)_levelProgressViewSource;

    public int Amount { get; private set; }
    public ICounter DeathCounter { get; private set; }

    private void OnValidate()
    {
        if (_levelProgressViewSource && !(_levelProgressViewSource is ICounterView))
        {
            Debug.LogError(_levelProgressViewSource + " not implement " + nameof(ICounterView));
            _levelProgressViewSource = null;
        }
    }

    public void Init(Character character, Config config, int currentLevel, IGame game)
    {
        Amount = (int)config.WaveSpawnerAmount.Evaluate(currentLevel);
        DeathCounter = new EnemiesDeathCounter(Amount, _levelProgressView, game);
        _botObjectPool.Init();
        _rewardObjectPool.Init();
        var botSpawner = new BotSpawner(_botObjectPool, character, DeathCounter, _rewardObjectPool);
        var waveInCircle = new SpawnWaveInCircle(config.WaveSpawnerStartRadius, config.WaveSpawnerDistanceBetweenCircles, config.WaveSpawnerStartAngleStep, botSpawner);
        _waveSpawner.Init(waveInCircle, Amount, (int)config.WaveSpawnerAmountInOnWave.Evaluate(currentLevel), config.WaveSpawnerInterval.Evaluate(currentLevel));
    }
}
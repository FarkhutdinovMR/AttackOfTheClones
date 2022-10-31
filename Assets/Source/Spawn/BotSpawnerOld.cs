using System.Collections;
using UnityEngine;

public class BotSpawnerOld : MonoBehaviour
{
    [field: SerializeField] public int Amount { get; private set; }

    [SerializeField] private Bot _bot;
    [SerializeField] private float _interval;
    [SerializeField] private float _intervalBetweenWaves;
    [SerializeField] private float _rowSpace;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _rewardTarget;
    [SerializeField] private AnimationCurve _waveAmountInTime;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _distanceToCharacter;

    private Counter _deathCounter;
    private int _currentAmount;

    public void Init(Counter deathCounter)
    {
        _deathCounter = deathCounter;
        enabled = true;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        bool isCompilted = false;
        while (isCompilted == false)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                float waveAmount = _waveAmountInTime.Evaluate((float)_currentAmount / Amount);
                isCompilted = SpawnWaveInCircle(waveAmount, _spawnPoints[i]);

                if (isCompilted)
                    yield break;

                yield return new WaitForSeconds(_interval);
            }

            yield return new WaitForSeconds(_intervalBetweenWaves);
        }
    }

    private bool SpawnWaveInCircle(float waveAmount, Transform point)
    {
        Vector2 spawnCenter = new Vector2(point.position.x, point.position.z);

        int count = 0;
        float _radius = 2f;
        float _angleStep = 90f;

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 360 / _angleStep; i++)
            {
                Vector2 spawnPoint = new Vector2(_radius * Mathf.Cos(_angleStep * i * Mathf.Deg2Rad), _radius * Mathf.Sin(_angleStep * i * Mathf.Deg2Rad));
                spawnPoint += spawnCenter;
                var spawnPosition = new Vector3(spawnPoint.x, 0f, spawnPoint.y);
                InstanceBot(spawnPosition);

                if (IncreaseCount())
                    return true;

                count++;
                if (count >= waveAmount)
                    return false;
            }

            _radius += 3f;
            _angleStep -= 360f / (_radius + _radius);
        }

        return false;
    }

    private Vector2 GetRandomPositionInAround(Vector3 position)
    {
        Vector2 spawnCenter = UnityEngine.Random.insideUnitCircle.normalized;
        spawnCenter *= _distanceToCharacter;
        return new Vector2(spawnCenter.x + _character.transform.position.x, spawnCenter.y + _character.transform.position.z);
    }

    private void InstanceBot(Vector3 position)
    {
        Bot newBot = Instantiate(_bot, position, Quaternion.identity, transform);
        newBot.Init(_character, _deathCounter);
    }

    private bool IncreaseCount()
    {
        _currentAmount++;
        if (_currentAmount >= Amount)
            return true;

        return false;
    }
}
using System.Collections;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [field: SerializeField] public int Amount { get; private set; }

    [SerializeField] private Bot _bot;
    [SerializeField] private float _interval;
    [SerializeField] private float _distanceToCharacter;
    [SerializeField] private float _rowSpace;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _rewardTarget;
    [SerializeField] private AnimationCurve _waveAmountInTime;

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
            float waveAmount = _waveAmountInTime.Evaluate((float)_currentAmount / Amount);
            isCompilted = SpawnCircle(waveAmount);
            yield return new WaitForSeconds(_interval);
        }
    }

    private bool SpawnRowsWave(float waveAmount)
    {
        uint row = (uint)Mathf.Sqrt(waveAmount);
        Vector2 spawnCenter = Random.insideUnitCircle.normalized;
        spawnCenter *= _distanceToCharacter;
        spawnCenter = new Vector2(spawnCenter.x + _character.transform.position.x, spawnCenter.y + _character.transform.position.z);

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < row; y++)
            {
                var spawnPosition = new Vector3(spawnCenter.x + _rowSpace * x, 0f, spawnCenter.y + _rowSpace * y);
                Bot newBot = Instantiate(_bot, spawnPosition, Quaternion.identity, transform);
                newBot.Init(_character, _deathCounter);

                _currentAmount++;
                if (_currentAmount >= Amount)
                    return true;
            }
        }

        return false;
    }

    private bool SpawnLine(float waveAmount)
    {
        Vector2 spawnCenter = Random.insideUnitCircle.normalized;
        spawnCenter *= _distanceToCharacter;
        spawnCenter = new Vector2(spawnCenter.x + _character.transform.position.x, spawnCenter.y + _character.transform.position.z);

        for (int x = 0; x < waveAmount; x++)
        {
            var spawnPosition = new Vector3(spawnCenter.x + _rowSpace * x, 0f, spawnCenter.y + _rowSpace * x);
            Bot newBot = Instantiate(_bot, spawnPosition, Quaternion.identity, transform);
            newBot.Init(_character, _deathCounter);

            _currentAmount++;
            if (_currentAmount >= Amount)
                return true;
        }

        return false;
    }

    private bool SpawnCircle(float waveAmount)
    {
        Vector2 spawnCenter = Random.insideUnitCircle.normalized;
        spawnCenter *= _distanceToCharacter;
        spawnCenter = new Vector2(spawnCenter.x + _character.transform.position.x, spawnCenter.y + _character.transform.position.z);

        for (int x = 0; x < waveAmount; x++)
        {
            Vector2 spawnPoint = Random.insideUnitCircle;
            spawnPoint *= waveAmount * 0.4f;
            spawnPoint += spawnCenter;

            var spawnPosition = new Vector3(spawnPoint.x, 0f, spawnPoint.y);
            Bot newBot = Instantiate(_bot, spawnPosition, Quaternion.identity, transform);
            newBot.Init(_character, _deathCounter);

            _currentAmount++;
            if (_currentAmount >= Amount)
                return true;
        }

        return false;
    }
}
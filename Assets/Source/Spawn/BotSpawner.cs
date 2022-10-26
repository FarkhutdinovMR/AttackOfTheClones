using System.Collections;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _bot;
    [SerializeField] private uint _waveAmount;
    [SerializeField] private uint _amountBotInWave;
    [SerializeField] private uint _interval;
    [SerializeField] private float _radius;
    [SerializeField] private float _rowSpace;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _rewardTarget;

    private uint _waveSpawned;

    private void Start()
    {
        StartCoroutine(Spaw());
    }

    private IEnumerator Spaw()
    {
        while (_waveSpawned < _waveAmount)
        {
            SpawnRowsWave();
            _waveSpawned++;
            yield return new WaitForSeconds(_interval);
        }
    }

    private void SpawnRowsWave()
    {
        uint row = (uint)Mathf.Sqrt(_amountBotInWave);
        Vector2 spawnCenter = Random.insideUnitCircle.normalized;
        spawnCenter *= _radius;
        spawnCenter = new Vector2(spawnCenter.x + _character.transform.position.x, spawnCenter.y + _character.transform.position.z);

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < row; y++)
            {
                var spawnPosition = new Vector3(spawnCenter.x + _rowSpace * x, 0f, spawnCenter.y + _rowSpace * y);
                Bot newBot = Instantiate(_bot, spawnPosition, Quaternion.identity, transform);
                newBot.Init(_character, _rewardTarget);
            }
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _intervalBetweenPoints;
    [SerializeField] private Transform[] _points;

    public int EnemyAmount { get; private set; }

    private ISpawnWave _spawnWave;
    private int _currentCount;
    private int _amount;
    private int _waveSize;
    private float _interval;

    public void Init(ISpawnWave spawnWave, int amount, int waveSize, float interval)
    {
        _spawnWave = spawnWave;
        _amount = amount;
        _waveSize = waveSize;
        _interval = interval;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while(_currentCount < _amount)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _waveSize = Math.Clamp(_waveSize, 0, _amount - _currentCount);
                _currentCount += _spawnWave.Spawn(_waveSize, _points[i].position);

                yield return new WaitForSeconds(_intervalBetweenPoints);
            }

            yield return new WaitForSeconds(_interval);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField] private float _intervalBetweenPoints;
    [SerializeField] private Transform[] _points;

    private ISpawnWave _spawnWave;
    private int _currentCount;

    private int _amount;
    private int _waveSize;

    public void Init(ISpawnWave spawnWave, int amount, int waveSize)
    {
        _spawnWave = spawnWave;
        _amount = amount;
        _waveSize = waveSize;
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
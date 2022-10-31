using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [field: SerializeField] public int Amount { get; private set; }
    [SerializeField] private float _interval;
    [SerializeField] private float _intervalBetweenPoints;
    [SerializeField] private AnimationCurve _waveSizeInTime;
    [SerializeField] private Transform[] _points;

    private ISpawnWave _spawnWave;
    private int _currentCount;
    private Coroutine _spawnCoroutine;

    public bool _isComplited => _currentCount >= Amount;

    public void Init(ISpawnWave spawnWave)
    {
        _spawnWave = spawnWave;
        StartSpawn();
    }

    private void StartSpawn()
    {
        _spawnCoroutine = StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while(_isComplited == false)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                int waveSize = (int)_waveSizeInTime.Evaluate((float)_currentCount / Amount);
                waveSize = Math.Clamp(waveSize, 0, Amount - _currentCount);
                _spawnWave.Spawn(waveSize, _points[i].position);
                _currentCount += waveSize;

                yield return new WaitForSeconds(_intervalBetweenPoints);
            }

            yield return new WaitForSeconds(_interval);
        }
    }
}
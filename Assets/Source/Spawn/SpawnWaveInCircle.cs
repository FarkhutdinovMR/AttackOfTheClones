using System;
using UnityEngine;

public class SpawnWaveInCircle : ISpawnWave
{
    private readonly float _startRadius = 2f;
    private readonly float _distanceBetweenCircles = 3f;
    private readonly float _startAngleStep = 90f;
    private readonly ISpawner _spawner;

    public SpawnWaveInCircle(float startRadius, float distanceBetweenCircles, float startAngleStep, ISpawner spawner)
    {
        _startRadius = startRadius;
        _distanceBetweenCircles = distanceBetweenCircles;
        _startAngleStep = startAngleStep;
        _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
    }

    public int Spawn(int size, Vector3 point)
    {
        int count = 0;
        float radius = _startRadius;
        float angleStep = _startAngleStep;

        while(count < size)
        {
            for (int i = 0; i < 360 / angleStep; i++)
            {
                var spawnPoint = new Vector3(radius * Mathf.Cos(angleStep * i * Mathf.Deg2Rad), 0f, radius * Mathf.Sin(angleStep * i * Mathf.Deg2Rad));
                spawnPoint += point;

                if (_spawner.Spawn(spawnPoint) == false)
                    return count;

                count++;

                if (count >= size)
                    return count;
            }

            radius += _distanceBetweenCircles;
            angleStep -= 360f / (radius + radius);
        }

        return count;
    }
}
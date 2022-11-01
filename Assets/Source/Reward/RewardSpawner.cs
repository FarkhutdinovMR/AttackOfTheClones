using System;
using UnityEngine;

public class RewardSpawner
{
    private readonly uint _reward;
    private readonly RewardObjectPool _pool;
    private readonly Transform _targetSource;

    public RewardSpawner(uint reward, Transform targetSource, RewardObjectPool pool)
    {
        _reward = reward;
        _targetSource = targetSource ?? throw new ArgumentNullException(nameof(targetSource));
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        Reward reward = _pool.GetPooledObject();
        if (reward == null)
            return;

        reward.Init(_reward, _targetSource);
        reward.gameObject.SetActive(true);
        reward.transform.SetPositionAndRotation(position, rotation);
    }
}
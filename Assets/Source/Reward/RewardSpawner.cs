using System;
using UnityEngine;

public class RewardSpawner
{
    private readonly uint _reward;
    private readonly Reward _rewardTemplate;
    private readonly Transform _targetSource;    

    public RewardSpawner(uint reward, Transform targetSource, Reward rewardTemplate)
    {
        _reward = reward;
        _targetSource = targetSource ?? throw new ArgumentNullException(nameof(targetSource));
        _rewardTemplate = rewardTemplate ?? throw new ArgumentNullException(nameof(rewardTemplate));
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        Reward newReward = MonoBehaviour.Instantiate(_rewardTemplate, position, rotation);
        newReward.Init(_reward, _targetSource);
    }
}
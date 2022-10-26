using BehaviorDesigner.Runtime;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private Health _health;
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private RewardSpawner _rewardSpawner;

    public void Init(Character character, Transform rewardTarget)
    {
        _behaviorTree.SetVariable("_player", (SharedCharacter)character);
        _rewardSpawner.Init(rewardTarget);
    }

    private void Start()
    {
        _health.Init(_config.BotHealth);
    }
}
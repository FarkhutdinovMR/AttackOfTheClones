using BehaviorDesigner.Runtime;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private BotInput _botInput;
    [SerializeField] private DamageVisualizer _damageRender;

    private RewardSpawner _rewardSpawner;

    public Health Health { get; private set; }

    private const string PlayerShared = "_player";

    public void Init(Character character, ICounter deathCounter, RewardObjectPool rewardObjectPool)
    {
        _rewardSpawner = new RewardSpawner(_config.RewardForBot, character.transform, rewardObjectPool);
        Health = new BotHealth(_config.BotHealth, _damageRender, _rewardSpawner, gameObject, deathCounter);
        _characterMovement.Init(_botInput);
        _damageRender.Init();
        _behaviorTree.SetVariable(PlayerShared, (SharedCharacter)character);
    }
}
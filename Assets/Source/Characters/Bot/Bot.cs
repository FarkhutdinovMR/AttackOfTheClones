using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private Reward _rewardTemplate;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private BotInput _botInput;
    [SerializeField] private ColorSwithcer _damageRender;

    private Counter _deathCounter;
    private RewardSpawner _rewardSpawner;

    public Health Health { get; private set; }

    private const string PlayerShared = "_player";

    public void Init(Character character, Counter deathCounter)
    {
        Health = new Health(_config.BotHealth);
        _characterMovement.Init(_botInput);
        _behaviorTree.SetVariable(PlayerShared, (SharedCharacter)character);
        _rewardSpawner = new RewardSpawner(_config.RewardForBot, character.transform, _rewardTemplate);
        _deathCounter = deathCounter;
        enabled = true;
    }

    private void OnEnable()
    {
        Health.Died += OnDied;
        Health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
        Health.Changed -= OnHealthChanged;
    }

    private void OnDied()
    {
        _rewardSpawner.Spawn(transform.position, transform.rotation);
        _deathCounter.Increase();
        Destroy(gameObject);
    }

    private void OnHealthChanged(int currentValue, int maxValue)
    {
        _damageRender.Switch();
    }
}
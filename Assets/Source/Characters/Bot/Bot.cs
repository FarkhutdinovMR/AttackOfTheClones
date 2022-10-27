using BehaviorDesigner.Runtime;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private Reward _rewardTemplate;

    private DeathCounter _deathCounter;
    private RewardSpawner _rewardSpawner;

    public Health Health { get; private set; }

    public void Init(Character character, Transform rewardTarget, DeathCounter deathCounter)
    {
        Health = new Health(_config.BotHealth);
        _deathCounter = deathCounter;
        _behaviorTree.SetVariable("_player", (SharedCharacter)character);
        _rewardSpawner = new RewardSpawner(_config.RewardForBot, character.transform, _rewardTemplate);
        enabled = true;
    }

    private void OnEnable()
    {
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
    }

    private void OnDied()
    {
        _rewardSpawner.Spawn(transform.position, transform.rotation);
        _deathCounter.Increase();
        Destroy(gameObject);
    }
}
using UnityEngine;

[RequireComponent (typeof(Health))]
public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private uint _value;
    [SerializeField] private Reward _rewardTemplate;

    private Transform _character;    
    private Health _health;

    public void Init(Transform character)
    {
        _character = character;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnBotDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnBotDied;
    }

    private void OnBotDied()
    {
        Reward newReward = Instantiate(_rewardTemplate, transform.position, transform.rotation);
        newReward.Init(_value, _character.transform);
    }
}
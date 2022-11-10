using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private StateConfig _damage;
    [SerializeField] private StateConfig _radius;
    [SerializeField] private StateConfig _interval;
    [SerializeField] private GetNearbyBot _targetSource;

    public List<State> States { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }
    public Inventory Inventory { get; private set; }

    public void Init(Config config, Saver.Data data)
    {
        Health = new Health(config.CharacterHealth);
        Level = data.CharacterLevel;
        Wallet = data.Wallet;
        Inventory = data.Inventory;
        States = data.CharacterState;
    }

    public List<State> GetDefaultStates()
    {
        return new List<State>() { new AttackDamage(_damage), new AttackRadius(_radius), new AttackInterval(_interval) };
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public List<StateConfig> StateConfigs;

    public List<State> States { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }

    public void Init(Config config, Saver.Data data)
    {
        States = new();
        States.Add(new AttackDamage(StateConfigs.Find(config => config.Type == StateType.AttackDamage), data.GetStateLevel(typeof(AttackDamage))));
        States.Add(new AttackInterval(StateConfigs.Find(config => config.Type == StateType.AttackInterval), data.GetStateLevel(typeof(AttackInterval))));
        States.Add(new AttackRadius(StateConfigs.Find(config => config.Type == StateType.AttackRadius), data.GetStateLevel(typeof(AttackRadius))));

        Health = new Health(config.CharacterHealth);
        Level = new CharacterLevel(data.CharacterLevel);
        Wallet = new Wallet(data.Gold);
    }
}
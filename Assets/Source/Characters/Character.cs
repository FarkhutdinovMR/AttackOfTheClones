using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public List<StateConfig> StateConfigs;

    [SerializeField] public List<Ability> _abilities;

    public List<State> States { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }
    public AbilityInventory Inventory { get; private set; }

    public void Init(Config config, Saver.Data data)
    {
        States = new();
        States.Add(new AttackDamage(StateConfigs.Find(config => config.Type == StateType.AttackDamage), data.GetStateLevel(typeof(AttackDamage))));
        States.Add(new AttackInterval(StateConfigs.Find(config => config.Type == StateType.AttackInterval), data.GetStateLevel(typeof(AttackInterval))));
        States.Add(new AttackRadius(StateConfigs.Find(config => config.Type == StateType.AttackRadius), data.GetStateLevel(typeof(AttackRadius))));

        Health = new Health(config.CharacterHealth);
        Level = data.CharacterLevel;
        Wallet = data.Wallet;

        var slots = new Slot[data.SlotDatas.Count];
        slots[0] = new Slot(_abilities.Find(ability => ability.GetType() == data.SlotDatas[0].Type), 1);
        slots[1] = new Slot(_abilities.Find(ability => ability.GetType() == data.SlotDatas[1].Type), 5);
        slots[2] = new Slot(_abilities.Find(ability => ability.GetType() == data.SlotDatas[2].Type), 10);
        slots[3] = new Slot(_abilities.Find(ability => ability.GetType() == data.SlotDatas[3].Type), 15);

        Inventory = new AbilityInventory(slots.ToList());
    }
}
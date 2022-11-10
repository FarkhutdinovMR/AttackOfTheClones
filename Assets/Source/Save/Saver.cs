using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private Config _defaultData;
    [SerializeField] private AbilityFactory _abilityFactory;
    [SerializeField] private Character _character;

    public Data PlayerData { get; protected set; } = new Data();

    public abstract void Save();

    protected abstract bool TryLoad();

    public virtual Data Load()
    {
        if (TryLoad() == false)
            SetDefaultData();

        return PlayerData;
    }

    private void SetDefaultData()
    {
        PlayerData = new Data()
        {
            Wallet = new Wallet(_defaultData.CharacterStartGold),
            CharacterLevel = new CharacterLevel(0, _defaultData.CharacterStartLevel, _defaultData.CharacterLevelUpCost),
        };

        PlayerData.CharacterState = _character.GetDefaultStates();
        var slots = new List<AbilitySlot>() { new AbilitySlot(1), new AbilitySlot(5), new AbilitySlot(10), new AbilitySlot(15) };
        PlayerData.Inventory = new(slots);
        Ability fireballAbility = _abilityFactory.CreateAbility<FireballAbility>();
        PlayerData.Inventory.AddAbility(fireballAbility);
        PlayerData.Inventory.Equip(fireballAbility, 0);
    }

    [Serializable]
    public class Data
    {
        public Wallet Wallet;
        public CharacterLevel CharacterLevel;
        public List<State> CharacterState;
        public Inventory Inventory;
    }
}
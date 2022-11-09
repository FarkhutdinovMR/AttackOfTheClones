using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private Config _defaultData;

    public Data PlayerData { get; protected set; } = new Data();

    public abstract void Save();

    protected abstract bool TryLoad();

    public virtual Data Load()
    {
        if (TryLoad() == false)
            SetDefaultData();

        return PlayerData;
    }

    public void SaveGold(uint value)
    {
        PlayerData.Gold = value;
    }

    public void SaveCharacterLevel(CharacterLevel characterLevel)
    {
        PlayerData.CharacterLevel = characterLevel;
    }

    public void SaveStates(IEnumerable<State> states)
    {
        PlayerData.StateDatas = new();
        foreach (State state in states)
            PlayerData.StateDatas.Add(new StateData(state.GetType(), state.Level));
    }

    public void SaveAbilityProducts(IEnumerable<AbilityProduct> products)
    {
        PlayerData.AbilityProductDatas = new();
        foreach (AbilityProduct product in products)
            PlayerData.AbilityProductDatas.Add(new AbilityProductData(product.Info.Ability.GetType(), product.IsBought));
    }

    public void SaveSlots(IEnumerable<Slot> slots)
    {
        PlayerData.SlotDatas = new();
        foreach (Slot slot in slots)
        {
            Type type = typeof(string);
            if (slot.Ability != null)
                type = slot.Ability.GetType();

            PlayerData.SlotDatas.Add(new SlotData(type));
        }
    }

    private void SetDefaultData()
    {
        PlayerData = new Data()
        {
            Gold = _defaultData.CharacterStartGold,
            CharacterLevel = new CharacterLevel(0, _defaultData.CharacterStartLevel, _defaultData.CharacterLevelUpCost),
            StateDatas = new(),
            AbilityProductDatas = new() { new AbilityProductData(typeof(FireballAbility), true)},
            SlotDatas = new() { new SlotData(typeof(FireballAbility)), new SlotData(typeof(string)), new SlotData(typeof(string)), new SlotData(typeof(string)) }
        };
    }

    [Serializable]
    public class Data
    {
        public uint Gold;
        public CharacterLevel CharacterLevel;
        public List<StateData> StateDatas;
        public List<AbilityProductData> AbilityProductDatas;
        public List<SlotData> SlotDatas;

        public uint GetStateLevel(Type type)
        {
            StateData result = StateDatas.Find(state => state.Type == type);

            if (result == null)
                return 1;

            return result.Level;
        }

        public bool GetAbilityProductsStatus(Type type)
        {
            AbilityProductData result = AbilityProductDatas.Find(state => state.Type == type);

            if (result == null)
                return false;

            return result.IsBought;
        }
    }

    [Serializable]
    public class StateData
    {
        public Type Type;
        public uint Level;

        public StateData(Type type, uint level)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Level = level;
        }
    }

    [SerializeField]
    public class AbilityProductData
    {
        public Type Type;
        public bool IsBought;

        public AbilityProductData(Type type, bool isBought)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            IsBought = isBought;
        }
    }

    [SerializeField]
    public class SlotData
    {
        public Type Type;

        public SlotData(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
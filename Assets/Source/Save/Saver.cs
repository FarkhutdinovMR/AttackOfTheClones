using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private Config _defaultData;
    [SerializeField] private AbilityProductInfo[] _productInfo;

    public Data PlayerData { get; protected set; } = new Data();

    public abstract void Save();

    protected abstract bool TryLoad();

    public virtual Data Load()
    {
        if (TryLoad() == false)
            SetDefaultData();

        return PlayerData;
    }

    public void SaveStates(IEnumerable<State> states)
    {
        PlayerData.StateDatas = new();
        foreach (State state in states)
            PlayerData.StateDatas.Add(new StateData(state.GetType(), state.Level));
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
            Wallet = new Wallet(_defaultData.CharacterStartGold),
            CharacterLevel = new CharacterLevel(0, _defaultData.CharacterStartLevel, _defaultData.CharacterLevelUpCost),
            StateDatas = new(),
            SlotDatas = new() { new SlotData(typeof(FireballAbility)), new SlotData(typeof(string)), new SlotData(typeof(string)), new SlotData(typeof(string)) }
        };

        PlayerData.AbilityProducts = new List<AbilityProduct>();
        foreach (AbilityProductInfo info in _productInfo)
            PlayerData.AbilityProducts.Add(new AbilityProduct(false, info));

        PlayerData.AbilityProducts.Find(product => product.Info.Ability.GetType() == typeof(FireballAbility)).Buy();
    }

    [Serializable]
    public class Data
    {
        public Wallet Wallet;
        public CharacterLevel CharacterLevel;
        public List<AbilityProduct> AbilityProducts;
        public List<StateData> StateDatas;
        public List<SlotData> SlotDatas;

        public uint GetStateLevel(Type type)
        {
            StateData result = StateDatas.Find(state => state.Type == type);

            if (result == null)
                return 1;

            return result.Level;
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

    [Serializable]
    public class SlotData
    {
        public Type Type;

        public SlotData(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
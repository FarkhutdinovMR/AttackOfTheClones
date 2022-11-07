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

    private void SetDefaultData()
    {
        PlayerData = new Data()
        {
            Gold = 0,
            CharacterLevel = new CharacterLevel(0, _defaultData.CharacterStartLevel, _defaultData.CharacterLevelUpCost),
            StateDatas = new()
        };
    }

    [Serializable]
    public class Data
    {
        public uint Gold;
        public CharacterLevel CharacterLevel;
        public List<StateData> StateDatas;

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
}
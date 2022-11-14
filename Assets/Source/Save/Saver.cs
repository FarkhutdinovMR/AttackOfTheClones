using System;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private Character _character;

    public Data PlayerData { get; protected set; } = new Data();

    public abstract void Save();

    public void SaveLevel(int index)
    {
        PlayerData.NextLevel = index;
    }

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
            Wallet = new Wallet(_config.CharacterStartGold),
            CharacterLevel = new CharacterLevel(0, _config.CharacterStartLevel, _config.CharacterLevelUpCost, _config.CharacterLevelProgress),
            CharacterState = _character.States,
            Inventory = _character.Inventory,
            NextLevel = _config.FirstLevelIndex
        };

        PlayerData.Inventory.Slots[0].Equip(PlayerData.Inventory.Abilities[0]);
    }

    [Serializable]
    public class Data
    {
        public Wallet Wallet;
        public CharacterLevel CharacterLevel;
        public State[] CharacterState;
        public Inventory Inventory;
        public int NextLevel;
    }
}
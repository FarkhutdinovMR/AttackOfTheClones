using System;
using UnityEngine;

public class PlayerPrefsSaver : Saver
{
    private const string Gold = nameof(Gold);
    private const string CharacterLevelValue = nameof(CharacterLevelValue);
    private const string CharacterLevelExp = nameof(CharacterLevelExp);
    private const string CharacterLevelUpCost = nameof(CharacterLevelUpCost);
    private const string Slot = nameof(Slot);

    private readonly string[] _stateDataKey =new[]{ 
        nameof(AttackDamage), nameof(AttackInterval), nameof(AttackRadius),
        nameof(FireballAttackDamage), nameof(FireballAttackInterval), nameof(FireballAttackRadius),
        nameof(IcePuddleAttackDamage), nameof(IcePuddleAttackInterval), nameof(IcePuddleAttackRadius),
        nameof(BallLightningAttackDamage), nameof(BallLightningAttackInterval), nameof(BallLightningAttackRadius) };

    private readonly string[] _productDataKey = new[]{
        nameof(BallLightningAbility), nameof(FireballAbility), nameof(IcePuddleAbility), "System.String"};

    private readonly string[] _slotDataKey = new[]{
        "Slot1", "Slot2", "Slot3", "Slot4"};

    public override void Save()
    {
        PlayerPrefs.SetInt(Gold, (int)PlayerData.Gold);

        PlayerPrefs.SetInt(CharacterLevelExp, (int)PlayerData.CharacterLevel.Exp);
        PlayerPrefs.SetInt(CharacterLevelValue, (int)PlayerData.CharacterLevel.Value);
        PlayerPrefs.SetInt(CharacterLevelUpCost, (int)PlayerData.CharacterLevel.LevelUpCost);

        foreach (StateData state in PlayerData.StateDatas)
        {
            string key = GetKey(_stateDataKey, state.Type);
            PlayerPrefs.SetInt(key,(int)state.Level);
        }

        foreach (AbilityProductData product in PlayerData.AbilityProductDatas)
        {
            string key = GetKey(_productDataKey, product.Type);
            PlayerPrefsSetBool(key, product.IsBought);
        }

        for (int i = 0; i < PlayerData.SlotDatas.Count; i++)
            PlayerPrefs.SetString(_slotDataKey[i], PlayerData.SlotDatas[i].Type.ToString());
    }

    protected override bool TryLoad()
    {
        if (PlayerPrefs.HasKey(Gold) == false)
            return false;

        PlayerData.Gold = (uint)PlayerPrefs.GetInt(Gold);
        PlayerData.CharacterLevel = new CharacterLevel((uint)PlayerPrefs.GetInt(CharacterLevelExp), (uint)PlayerPrefs.GetInt(CharacterLevelValue), (uint)PlayerPrefs.GetInt(CharacterLevelUpCost));

        PlayerData.StateDatas = new();
        foreach (string key in _stateDataKey)
        {
            Type type = GetType(key);
            PlayerData.StateDatas.Add(new StateData(type, (uint)PlayerPrefs.GetInt(key)));
        }

        PlayerData.AbilityProductDatas = new();
        foreach (string key in _productDataKey)
        {
            Type type = GetType(key);
            PlayerData.AbilityProductDatas.Add(new AbilityProductData(type, PlayerPrefsGetBool(key)));
        }

        PlayerData.SlotDatas = new();
        foreach (string key in _slotDataKey)
        {
            Type type = GetType(PlayerPrefs.GetString(key));
            PlayerData.SlotDatas.Add(new SlotData(type));
        }

        return true;
    }

    private string GetKey(string[] keys, Type type)
    {
        string key = Array.Find(keys, s => s == type.ToString());

        if (string.IsNullOrEmpty(key))
            throw new InvalidOperationException($"Key: {type} was not found.");

        return key;
    }

    private Type GetType(string key)
    {
        Type type = Type.GetType(key);
        if (type == null)
            throw new InvalidOperationException($"Type: {key} was not found.");

        return type;
    }

    private void PlayerPrefsSetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    private bool PlayerPrefsGetBool(string key)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(key));
    }

    [ContextMenu("Remove all save data")]
    public void RemoveAllSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
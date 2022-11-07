using System;
using UnityEngine;

public class PlayerPrefsSaver : Saver
{
    private const string Gold = nameof(Gold);
    private const string CharacterLevelValue = nameof(CharacterLevelValue);
    private const string CharacterLevelExp = nameof(CharacterLevelExp);
    private const string CharacterLevelUpCost = nameof(CharacterLevelUpCost);

    private readonly string[] _stateDataKey =new[]{ 
        nameof(AttackDamage), nameof(AttackInterval), nameof(AttackRadius),
        nameof(FireballAttackDamage), nameof(FireballAttackInterval), nameof(FireballAttackRadius),
        nameof (IcePuddleAttackDamage), nameof(IcePuddleAttackInterval), nameof(IcePuddleAttackRadius) };

    public override void Save()
    {
        PlayerPrefs.SetInt(Gold, (int)PlayerData.Gold);

        PlayerPrefs.SetInt(CharacterLevelExp, (int)PlayerData.CharacterLevel.Exp);
        PlayerPrefs.SetInt(CharacterLevelValue, (int)PlayerData.CharacterLevel.Value);
        PlayerPrefs.SetInt(CharacterLevelUpCost, (int)PlayerData.CharacterLevel.LevelUpCost);

        foreach (StateData state in PlayerData.StateDatas)
        {
            string key = Array.Find(_stateDataKey, s => s == state.Type.ToString());

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException($"Key: {state.Type} was not found.");

            PlayerPrefs.SetInt(key, (int)state.Level);
        }
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
            Type type = Type.GetType(key);

            if (type == null)
                throw new InvalidOperationException($"Type: {key} was not found.");

            PlayerData.StateDatas.Add(new StateData(type, (uint)PlayerPrefs.GetInt(key)));
        }

        return true;
    }

    [ContextMenu("Remove all save data")]
    public void RemoveAllSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
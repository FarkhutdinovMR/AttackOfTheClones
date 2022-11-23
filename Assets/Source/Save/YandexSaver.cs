using Agava.YandexGames;
using UnityEngine;

public class YandexSaver : Saver
{
    public YandexSaver(Config config, Character character) : base(config, character) { }

    public override void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerData);
        PlayerAccount.SetPlayerData(jsonString);
    }

    protected override bool TryLoad()
    {
        string jsonString = "";
        PlayerAccount.GetPlayerData((data) => jsonString = data);

        if (string.IsNullOrEmpty(jsonString))
            return false;

        PlayerData = JsonUtility.FromJson<Data>(jsonString);
        return true;
    }
}
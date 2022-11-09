using Agava.YandexGames;
using UnityEngine;

public class YandexSaver : Saver
{
    public override void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerData);
        PlayerAccount.SetPlayerData(jsonString);
    }

    protected override bool TryLoad()
    {
        string jsonString = "";
        PlayerAccount.GetPlayerData((data) => jsonString = data);
        PlayerData = JsonUtility.FromJson<Data>(jsonString);

        return true;
    }
}
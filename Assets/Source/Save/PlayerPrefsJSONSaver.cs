using UnityEngine;

public class PlayerPrefsJSONSaver : Saver
{
    private const string SaveKey = nameof(SaveKey);

    public override void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString(SaveKey, jsonString);
    }

    protected override bool TryLoad()
    {
        if (PlayerPrefs.HasKey(SaveKey) == false)
            return false;

        string jsonString = PlayerPrefs.GetString(SaveKey);
        PlayerData = JsonUtility.FromJson<Data>(jsonString);
        return true;
    }

    [ContextMenu("Remove all save data")]
    public void RemoveAllSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
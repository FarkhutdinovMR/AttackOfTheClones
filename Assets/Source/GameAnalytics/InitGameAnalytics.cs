using GameAnalyticsSDK;
using UnityEngine;

public class InitGameAnalytics : MonoBehaviour
{
    private const string GameStartCount = "GameStartCount";

    private void Start()
    {
#if !YANDEX_GAMES
        GameAnalytics.Initialize();
        int count = PlayerPrefs.GetInt(GameStartCount);
        count++;
        PlayerPrefs.SetInt(GameStartCount, count);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", count);
#endif
    }
}
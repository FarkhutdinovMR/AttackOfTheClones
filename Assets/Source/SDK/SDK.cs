#pragma warning disable

using Agava.VKGames;
using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class SDK : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Language _language;
    [SerializeField] private LeaderboardView _leaderboardView;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        while (true)
        {
#if YANDEX_GAMES
            if (YandexGamesSdk.IsInitialized)
#endif
#if VK_GAMES
            if (VKGamesSdk.Initialized)
#endif
            {
                OnSDKInitilized();
                yield break;
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void OnSDKInitilized()
    {
#if YANDEX_GAMES
        _language.Set(YandexGamesSdk.Environment.i18n.lang);
#endif
        _leaderboardView.Show();
    }

    public void InviteFriends()
    {
        SocialInteraction.InviteFriends(OnRewardedCallback);
    }

    private void OnRewardedCallback()
    {
        _character.Wallet.Add(50);
    }
}
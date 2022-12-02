#pragma warning disable

using Agava.VKGames;
using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class InitSDK : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        DontDestroyOnLoad(gameObject);

#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());
#endif
#if VK_GAMES
        yield return VKGamesSdk.Initialize();
#endif
    }
}
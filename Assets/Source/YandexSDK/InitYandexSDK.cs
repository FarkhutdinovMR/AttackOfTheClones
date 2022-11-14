#pragma warning disable

using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class InitYandexSDK : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        DontDestroyOnLoad(gameObject);
        yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());
    }
}
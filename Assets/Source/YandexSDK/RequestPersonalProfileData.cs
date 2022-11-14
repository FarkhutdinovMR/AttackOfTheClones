using Agava.YandexGames;
using UnityEngine;

public class RequestPersonalProfileData : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission();
#endif
    }
}
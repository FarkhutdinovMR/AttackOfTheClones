using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class YandexAd : MonoBehaviour
{
    private Action _onEndCallback;
    private Action<bool> _onVideoAdCallback;
    private bool _isRewarded;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();
    }

    public void ShowInterstitialAd(Action OnEndCallback)
    {
        _onEndCallback = OnEndCallback;

#if !UNITY_WEBGL || UNITY_EDITOR
        _onEndCallback?.Invoke();
        return;
#endif

        InterstitialAd.Show(null, OnInterstitialAdClose, OnInterstitialAdError, OnInterstitialAdOffline);
    }

    public void ShowVideoAd(Action<bool> onVideoAdCallback)
    {
        _onVideoAdCallback = onVideoAdCallback;

#if !UNITY_WEBGL || UNITY_EDITOR
        _onVideoAdCallback?.Invoke(false);
        return;
#endif

        _isRewarded = false;
        VideoAd.Show(null, OnVideoAdRewarded, OnVideoAdClose, OnVideoAdError);
    }

    private void OnInterstitialAdClose(bool value)
    {
        _onEndCallback?.Invoke();
    }

    private void OnInterstitialAdError(string value)
    {
        _onEndCallback?.Invoke();
    }

    private void OnInterstitialAdOffline()
    {
        _onEndCallback?.Invoke();
    }

    private void OnVideoAdRewarded()
    {
        _isRewarded = true;
    }

    private void OnVideoAdClose()
    {
        _onVideoAdCallback?.Invoke(_isRewarded);
    }

    private void OnVideoAdError(string error)
    {
        _onVideoAdCallback?.Invoke(_isRewarded);
    }
}
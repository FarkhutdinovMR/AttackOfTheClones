#pragma warning disable

using Agava.YandexGames;
using System;
using UnityEngine;

public class YandexAd : MonoBehaviour
{
    [SerializeField] private Audio _audio;

    private Action _onEndCallback;
    private Action<bool> _onVideoAdCallback;
    private bool _isRewarded;

    public void ShowInterstitialAd(Action OnEndCallback)
    {
        _onEndCallback = OnEndCallback;

#if !UNITY_WEBGL || UNITY_EDITOR
        _onEndCallback?.Invoke();
        return;
#endif

        InterstitialAd.Show(OnInterstitialAdOpen, OnInterstitialAdClose, OnInterstitialAdError, OnInterstitialAdOffline);
    }

    public void ShowVideoAd(Action<bool> onVideoAdCallback)
    {
        _onVideoAdCallback = onVideoAdCallback;

#if !UNITY_WEBGL || UNITY_EDITOR
        _onVideoAdCallback?.Invoke(false);
        return;
#endif

        _isRewarded = false;
        VideoAd.Show(OnVideoAdOpen, OnVideoAdRewarded, OnVideoAdClose, OnVideoAdError);
    }

    private void OnInterstitialAdOpen()
    {
        _audio.AddMute();
    }

    private void OnInterstitialAdClose(bool value)
    {
        _onEndCallback?.Invoke();
        _audio.RemoveMute();
    }

    private void OnInterstitialAdError(string value)
    {
        _onEndCallback?.Invoke();
    }

    private void OnInterstitialAdOffline()
    {
        _onEndCallback?.Invoke();
    }

    private void OnVideoAdOpen()
    {
        _audio.AddMute();
    }

    private void OnVideoAdRewarded()
    {
        _isRewarded = true;
    }

    private void OnVideoAdClose()
    {
        _onVideoAdCallback?.Invoke(_isRewarded);
        _audio.RemoveMute();
}

    private void OnVideoAdError(string error)
    {
        _onVideoAdCallback?.Invoke(_isRewarded);
    }
}
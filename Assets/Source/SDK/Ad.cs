#pragma warning disable

using Agava.VKGames;
using Agava.YandexGames;
using System;
using UnityEngine;

public class Ad : MonoBehaviour
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

#if YANDEX_GAMES
        if (YandexGamesSdk.IsInitialized == false)
        {
            _onEndCallback?.Invoke();
            return;
        }
        InterstitialAd.Show(OnInterstitialAdOpen, OnInterstitialAdClose, OnInterstitialAdError, OnInterstitialAdOffline);
#endif
#if VK_GAMES
        if (VKGamesSdk.Initialized == false)
        {
            _onEndCallback?.Invoke();
            return;
        }
        Agava.VKGames.Interstitial.Show(OnVKGamesInterstitialOpen, OnInterstitialAdError);
#endif
    }

    public void ShowVideoAd(Action<bool> onVideoAdCallback)
    {
        _onVideoAdCallback = onVideoAdCallback;
        _isRewarded = false;

#if !UNITY_WEBGL || UNITY_EDITOR
        _onVideoAdCallback?.Invoke(_isRewarded);
        return;
#endif

#if YANDEX_GAMES
        if (YandexGamesSdk.IsInitialized == false)
        {
            _onVideoAdCallback?.Invoke(_isRewarded);
            return;
        }
        Agava.YandexGames.VideoAd.Show(OnVideoAdOpen, OnVideoAdRewarded, OnVideoAdClose, OnVideoAdError);
#endif
#if VK_GAMES
        if (VKGamesSdk.Initialized == false)
        {
            _onVideoAdCallback?.Invoke(_isRewarded);
            return;
        }
        Agava.VKGames.VideoAd.Show(OnVKGamesVideoAdRewarded, OnVideoAdError);
#endif
    }

    private void OnInterstitialAdOpen()
    {
        _audio.AddMute();
    }

    private void OnVKGamesInterstitialOpen()
    {
        _onEndCallback?.Invoke();
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

    private void OnInterstitialAdError()
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

    private void OnVKGamesVideoAdRewarded()
    {
        _isRewarded = true;
        _onVideoAdCallback?.Invoke(_isRewarded);
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

    private void OnVideoAdError()
    {
        _onVideoAdCallback?.Invoke(_isRewarded);
    }
}
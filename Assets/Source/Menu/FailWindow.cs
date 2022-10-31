using System;
using UnityEngine;

public class FailWindow : MonoBehaviour
{
    [SerializeField] private YandexAd _yandexAd;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Character _character;

    private Action _onResurrected;

    public void Open(Action onResurrectedCallback)
    {
        gameObject.SetActive(true);
        _onResurrected = onResurrectedCallback;
    }

    public void OnRestartButtonClicked()
    {
        _yandexAd.ShowInterstitialAd(OnInterstitialAdEnd);
    }

    public void OnRessurectionButtonClicked()
    {
        _yandexAd.ShowVideoAd(OnVideoAdRewarded);
    }

    private void OnInterstitialAdEnd()
    {
        Close();
        _sceneLoader.Restart();
    }

    private void OnVideoAdRewarded(bool result)
    {
        if (result == false)
            return;

        _character.Health.Resurrect();
        Close();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        _onResurrected?.Invoke();
    }
}
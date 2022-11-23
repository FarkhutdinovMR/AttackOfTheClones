using System;
using UnityEngine;

public class FailWindow : MonoBehaviour
{
    [SerializeField] private YandexAd _yandexAd;
    [SerializeField] private Character _character;

    private Action<bool> _onResurrected;

    public void Open(Action<bool> onResurrectedCallback)
    {
        gameObject.SetActive(true);
        _onResurrected = onResurrectedCallback;
    }

    public void OnRestartButtonClick()
    {
        _yandexAd.ShowInterstitialAd(Close);
    }

    public void OnRessurectionButtonClick()
    {
        _yandexAd.ShowVideoAd(OnVideoAdRewarded);
    }

    private void OnVideoAdRewarded(bool result)
    {
        if (result == false)
            return;

        _character.Health.Resurrect();
        Close(result);
    }

    private void Close(bool result)
    {
        gameObject.SetActive(false);
        _onResurrected?.Invoke(result);
    }

    private void Close()
    {
        Close(false);
    }
}
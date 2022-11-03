using CompositeRoot;
using System;
using UnityEngine;

public class WinWndow : MonoBehaviour
{
    [SerializeField] private YandexAd _yandexAd;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private TextView _goldView;

    private Action _onClose;

    public void Open(uint gold, Action onClose)
    {
        gameObject.SetActive(true);
        _onClose = onClose;
        _goldView.Render(gold);
    }

    public void OnNextLevelButtonClicked()
    {
        _yandexAd.ShowInterstitialAd(OnInterstitialAdEnd);
    }

    private void OnInterstitialAdEnd()
    {
        Close();
        _sceneLoader.LoadNext();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        _onClose?.Invoke();
    }
}
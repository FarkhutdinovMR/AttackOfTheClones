using System;
using UnityEngine;

public class WinWndow : MonoBehaviour
{
    [SerializeField] private Ad _adSDK;
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
        _adSDK.ShowInterstitialAd(Close);
    }

    private void Close()
    {
        gameObject.SetActive(false);
        _onClose?.Invoke();
    }
}
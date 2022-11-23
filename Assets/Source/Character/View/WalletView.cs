using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WalletView : MonoBehaviour, IWalletView
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void Render(uint value)
    {
        _text.SetText(value.ToString());
    }
}
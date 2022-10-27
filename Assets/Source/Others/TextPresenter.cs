using UnityEngine;
using TMPro;

[RequireComponent (typeof(TMP_Text))]
public class TextPresenter : MonoBehaviour
{
    private TMP_Text _text;
    private string _prefix;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _prefix = _text.text;
    }

    public void Render(uint value)
    {
        _text.SetText(_prefix + value.ToString());
    }
}
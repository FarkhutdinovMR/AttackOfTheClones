using UnityEngine;
using TMPro;

[RequireComponent (typeof(TMP_Text))]
public class TextView : MonoBehaviour
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
        Render(value.ToString());
    }

    public void Render(int value)
    {
        Render(value.ToString());
    }

    private void Render(string value)
    {
        _text.text = _prefix + value;
    }
}
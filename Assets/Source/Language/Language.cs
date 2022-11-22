using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private Dictionary<string, string> _languageISO639_1Codes = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "tr", "Turkish" },
    };

    public void Set(string value)
    {
        if (_languageISO639_1Codes.ContainsKey(value))
            _leanLocalization.SetCurrentLanguage(_languageISO639_1Codes[value]);
    }
}
using Lean.Localization;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _stateTemplate;
    [SerializeField] private Transform _statesContainer;

    private List<TMP_Text> _states = new();

    public void Render(AbilityData ability)
    {
        string localizedName = LeanLocalization.GetTranslationText(ability.Name, ability.Name);
        _name.SetText(localizedName);
        _icon.sprite = ability.Icon;

        Clear();

        foreach (State state in ability.States)
        {
            TMP_Text newText = Instantiate(_stateTemplate, _statesContainer);
            string localizedStateType = state.StateType.ToString();
            localizedStateType = LeanLocalization.GetTranslationText(localizedStateType, localizedStateType);
            newText.SetText($"{localizedStateType} {state.Value}");
            _states.Add(newText);
        }
    }

    private void Clear()
    {
        foreach (TMP_Text text in _states)
            Destroy(text.gameObject);

        _states.Clear();
    }
}
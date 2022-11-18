using Lean.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _stateType;
    [SerializeField] private TMP_Text _stateName;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _stateLevel;
    [SerializeField] private Button _selectButton;

    private const string Lvl = "Lvl";

    private Action Completed;

    public AbilityUpgrade.UpgradeCard Card { get; private set; }

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
    }

    public void Init(AbilityUpgrade.UpgradeCard card, Action callback)
    {
        Card = card ?? throw new ArgumentNullException(nameof(card));
        Completed = callback;
        Render();
    }

    private void Render()
    {
        string localizedName = LeanLocalization.GetTranslationText(Card.Name, Card.Name);
        _stateType.SetText(localizedName);

        string localizedStateType = Card.State.StateType.ToString();
        localizedStateType = LeanLocalization.GetTranslationText(localizedStateType, localizedStateType);
        string sign = Card.State.UpgradeModificator > 0 ? "+" : "";
        _stateName.SetText($"{localizedStateType}{sign}{Card.State.UpgradeModificator}");

        string localizedLvl = LeanLocalization.GetTranslationText(Lvl, Lvl);
        _stateLevel.SetText($"{localizedLvl} {Card.State.Level}");

        _icon.sprite = Card.State.Icon;
    }

    private void OnSelectButtonClicked()
    {
        Card.State.Upgrade();
        Completed?.Invoke();
    }
}
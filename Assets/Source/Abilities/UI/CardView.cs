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
        _stateType.SetText(Card.Name);
        string sign = Card.State.UpgradeModificator > 0 ? "+" : "";
        _stateName.SetText($"{Card.State.StateType}  {sign}{Card.State.UpgradeModificator}");
        _icon.sprite = Card.State.Icon;
        _stateLevel.SetText(_stateLevel.text + Card.State.Level);
    }

    private void OnSelectButtonClicked()
    {
        Card.State.Upgrade();
        Completed?.Invoke();
    }
}
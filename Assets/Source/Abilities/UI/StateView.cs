using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateView : MonoBehaviour
{
    [SerializeField] private TMP_Text _stateType;
    [SerializeField] private TMP_Text _stateName;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _stateLevel;
    [SerializeField] private Button _selectButton;

    private Action Completed;

    public State State { get; private set; }

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
    }

    public void Init(State state, Action callback)
    {
        State = state ?? throw new ArgumentNullException(nameof(state));
        Completed = callback;
        Render();
    }

    private void Render()
    {
        _stateType.SetText(State.Type.ToString());
        _stateName.SetText(State.Name + " " + State.Upgrade.Modificator);
        _icon.sprite = State.Icon;
        _stateLevel.SetText(_stateLevel.text + State.Upgrade.Level);
    }

    private void OnSelectButtonClicked()
    {
        State.Upgrade.Upgrade();
        Completed?.Invoke();
    }
}
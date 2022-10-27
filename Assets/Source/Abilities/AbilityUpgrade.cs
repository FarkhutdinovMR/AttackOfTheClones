using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject _abilityUpgradeWindow;
    [SerializeField] private Transform _container;
    [SerializeField] private StateView _stateTemplate;
    [SerializeField] private AbilityFactory _abilityFactory;

    private const int StateAmount = 3;

    private List<State> _states = new();
    private List<StateView> _stateViews = new();
    private Action Completed;

    private void Start()
    {
        foreach(Ability ability in _abilityFactory.Abilities)
        {
            foreach (State state in ability.States)
                _states.Add(state);
        }

        foreach (State state in _abilityFactory.BaseStates)
            _states.Add(state);
    }

    public void OpenUpgradeWindow(Action callback)
    {
        Completed = callback;
        _abilityUpgradeWindow.SetActive(true);

        if (_stateViews.Count > 0)
            Clear();

        for (int i = 0; i < StateAmount; i++)
        {
            State state = GetRandomState();
            StateView newStateView = Instantiate(_stateTemplate, _container);
            newStateView.Init(state, OnStateUpgraded);
            _stateViews.Add(newStateView);
        }
    }

    private void Clear()
    {
        foreach (StateView stateView in _stateViews)
            Destroy(stateView.gameObject);

        _stateViews.Clear();
    }

    private State GetRandomState()
    {
        int randomStateIndex = 0;

        for (int i = 0; i < 10; i++)
        {
            randomStateIndex = UnityEngine.Random.Range(0, _states.Count);
            if (_stateViews.Find(item => item.State == _states[randomStateIndex]) == null)
                break;
        }

        return _states[randomStateIndex];
    }

    private void OnStateUpgraded()
    {
        _abilityUpgradeWindow.SetActive(false);
        Completed?.Invoke();
    }
}
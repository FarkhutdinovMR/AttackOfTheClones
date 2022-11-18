using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUpgrade : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private CardView _cardTemplate;
    [SerializeField] private TextView _levelPresenter;
    [SerializeField] private Character _character;

    private const int CardAmount = 3;

    private List<UpgradeCard> _cards = new();
    private List<CardView> _cardViews = new();
    private Action _onWindowCloseCallback;

    public void Init()
    {
        foreach (Slot slot in _character.Inventory.Slots)
        {
            if (slot.IsEmpty)
                continue;

            foreach (State state in slot.Ability.States)
                _cards.Add(new UpgradeCard(slot.Ability.Name, state));
        }

        foreach (State state in _character.States)
            _cards.Add(new UpgradeCard(state.Name, state));
    }

    public void OpenUpgradeWindow(Action onWindowCloseCallback)
    {
        _onWindowCloseCallback = onWindowCloseCallback;
        gameObject.SetActive(true);
        _levelPresenter.Render(_character.Level.Value);

        if (_cardViews.Count > 0)
            Clear();

        for (int i = 0; i < CardAmount; i++)
        {
            UpgradeCard card = GetRandomState();
            CardView newStateView = Instantiate(_cardTemplate, _container);
            newStateView.Init(card, OnStateUpgraded);
            _cardViews.Add(newStateView);
        }
    }

    private void Clear()
    {
        foreach (CardView stateView in _cardViews)
            Destroy(stateView.gameObject);

        _cardViews.Clear();
    }

    private UpgradeCard GetRandomState()
    {
        int randomStateIndex = 0;

        for (int i = 0; i < 10; i++)
        {
            randomStateIndex = UnityEngine.Random.Range(0, _cards.Count);
            if (_cardViews.Find(item => item.Card.State == _cards[randomStateIndex].State) == null)
                break;
        }

        return _cards[randomStateIndex];
    }

    private void OnStateUpgraded()
    {
        gameObject.SetActive(false);
        _onWindowCloseCallback?.Invoke();
    }

    public class UpgradeCard
    {
        public string Name;
        public State State;

        public UpgradeCard(string name, State state)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
    }
}
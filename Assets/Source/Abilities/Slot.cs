using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Slot
{
    private List<State> _states;

    public Slot(uint unlockLevel)
    {
        UnlockLevel = unlockLevel;
    }

    [field: SerializeReference] public AbilityData Ability { get; private set; }
    [field: SerializeField] public uint UnlockLevel { get; private set; }
    public bool IsEmpty => Ability == null;

    public void Equip(AbilityData ability)
    {
        Ability = ability ?? throw new ArgumentNullException(nameof(ability));
    }

    public float GetValue(StateType stateType)
    {
        List<State> result = _states.FindAll(state => state.StateType == stateType);
        float value = 0;
        foreach (State newState in result)
            value += newState.Value;

        return value;
    }

    public void AddStates(State[] states)
    {
        foreach (State state in states)
            AddState(state);
    }

    public void AddState(State state)
    {
        if (_states == null)
            _states = new();

        if (_states.Contains(state))
            throw new InvalidOperationException();

        _states.Add(state);
    }
}
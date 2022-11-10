using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilitySlot
{
    [SerializeReference] private List<State> _states;

    public AbilitySlot(uint unlockLevel)
    {
        _states = new();
        UnlockLevel = unlockLevel;
    }

    [field: SerializeReference] public Ability Ability { get; private set; }
    [field: SerializeField] public uint UnlockLevel { get; private set; }
    public bool IsEmpty => Ability == null;

    public void Equip(Ability ability)
    {
        if (Ability != null)
            RemoveStates(Ability.States);

        Ability = ability ?? throw new ArgumentNullException(nameof(ability));
        AddStates(Ability.States);
    }

    public float GetValue(Type stateType)
    {
        List<State> result = _states.FindAll(state => state.GetType() == stateType);
        float value = 0;
        foreach(State newState in result)
            value += newState.Value;

        return value;
    }

    public void AddStates(List<State> states)
    {
        foreach (State state in states)
            AddState(state);
    }

    public void AddState(State state)
    {
        if (_states.Contains(state))
            throw new InvalidOperationException();

        _states.Add(state);
    }

    public void RemoveStates(List<State> states)
    {
        foreach (State state in states)
            RemoveState(state);
    }

    public void RemoveState(State state)
    {
        if (_states.Contains(state) == false)
            throw new InvalidOperationException();

        _states.Remove(state);
    }
}
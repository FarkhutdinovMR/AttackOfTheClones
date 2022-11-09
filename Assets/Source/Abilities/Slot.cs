using System;
using System.Collections.Generic;

public class Slot
{
    private readonly List<State> _states;

    public Slot(Ability ability, uint unlockLevel)
    {
        _states = new();
        Ability = ability;
        UnlockLevel = unlockLevel;
    }

    public Ability Ability { get; private set; }
    public uint UnlockLevel { get; private set; }
    public bool IsEmpty => Ability == null;

    public void Equip(Ability ability)
    {
        Ability = ability ?? throw new ArgumentNullException(nameof(ability));
    }

    public float GetValue(StateType stateType)
    {
        List<State> result = _states.FindAll(s => s.Config.Type == stateType);
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

    public void RemoveState(State state)
    {
        if (_states.Contains(state) == false)
            throw new InvalidOperationException();

        _states.Remove(state);
    }

    public void Clear()
    {
        _states.Clear();
    }
}
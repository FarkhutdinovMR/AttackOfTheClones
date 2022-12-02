using System;
using UnityEngine;

[Serializable]
public class StateProduct : IStateProduct
{
    public StateProduct(NewState state, uint costStep, uint cost)
    {
        State = state ?? throw new ArgumentNullException(nameof(state));
        CostStep = costStep;
        Cost = cost;
    }

    [field: SerializeField] public uint Cost { get; private set; }
    [field: SerializeField] public uint CostStep { get; private set; }
    [field: SerializeField] public NewState State { get; private set; }

    public bool TryBuy()
    {
        if (State.Level.CanLevelUp == false)
            return false;

        State.Level.LevelUp();
        Cost += CostStep;
        return true;
    }
}

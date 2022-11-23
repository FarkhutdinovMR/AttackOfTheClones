using System;
using UnityEngine;

[Serializable]
public class Score : IScore
{
    public Score(uint value)
    {
        Value = value;
    }

    [field: SerializeField] public uint Value { get; private set; }

    public virtual void Add(uint value)
    {
        Value += value;
    }
}
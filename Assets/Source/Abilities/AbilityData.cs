using System;
using UnityEngine;

[Serializable]
public class AbilityData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public uint Cost { get; private set; }
    [field: SerializeField] public GameObject Template { get; private set; }
    [field: SerializeField] public AbilityStatus Status { get; private set; }
    [field: SerializeField] public NewState[] State { get; private set; }
}

public enum AbilityStatus
{
    NotBought,
    Inventory,
    Active
}
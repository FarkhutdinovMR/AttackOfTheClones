using System;
using UnityEngine;

[Serializable]
public class NewSlot
{
    [field: SerializeField] public uint UnlockLevel { get; private set; }
    [SerializeField] public AbilityData Ability { get; private set; }
}
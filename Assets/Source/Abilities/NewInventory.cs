using System;
using UnityEngine;

[Serializable]
public class NewInventory
{
    [field: SerializeField] public AbilityData[] Data { get; private set; }
    [field: SerializeField] public NewSlot[] Slot { get; private set; }
}
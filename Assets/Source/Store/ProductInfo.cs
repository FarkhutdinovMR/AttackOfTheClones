using UnityEngine;

public class ProductInfo : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public uint Cost { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
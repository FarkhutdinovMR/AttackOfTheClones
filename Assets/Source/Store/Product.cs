using UnityEngine;

[CreateAssetMenu (fileName = "NewProduct", menuName = "Product")]
public class Product : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public uint Cost { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
using UnityEngine;

[CreateAssetMenu (fileName = "NewState", menuName = "ScriptableObjects/State")]
public class State : ScriptableObject
{
    [field: SerializeField] public float DefaultValue { get; private set; }
    [field: SerializeField] public float UpgradeMultiplier { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    private uint _level = 1;

    public uint Level => _level;
    public float Value => DefaultValue + (Level - 1) * UpgradeMultiplier;

    public void Upgrade() => _level++;
}
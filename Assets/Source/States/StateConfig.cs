using UnityEngine;

[CreateAssetMenu(fileName = "NewStateConfig", menuName = "StateConfig", order = 51)]
public class StateConfig : ScriptableObject
{
    [field: SerializeField] public StateType Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float MaxLevel { get; private set; }
    [field: SerializeField] public float UpgradeModificator { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}

public enum StateType
{
    AttackDamage,
    AttackRadius,
    AttackInterval
}
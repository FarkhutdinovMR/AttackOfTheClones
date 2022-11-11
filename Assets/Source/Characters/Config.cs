using UnityEngine;

[CreateAssetMenu (fileName = "NewConfig", menuName = "ScriptableObjects/Config")]
public class Config : ScriptableObject
{
    [field: SerializeField] public int FirstLevelIndex { get; private set; }
    [field: SerializeField] public int CharacterHealth { get; private set; }
    [field: SerializeField] public uint CharacterStartLevel { get; private set; }
    [field: SerializeField] public uint CharacterStartGold { get; private set; }
    [field: SerializeField] public uint CharacterLevelUpCost { get; private set; }
    [field: SerializeField] public float CharacterLevelProgress { get; private set; }
    [field: SerializeField] public uint CharacterStateStartLevel { get; private set; }
    [field: SerializeField] public uint CharacterAbilityInventoryCapacity { get; private set; }
    [field: SerializeField] public int BotHealth { get; private set; }
    [field: SerializeField] public uint RewardForBot { get; private set; }
    [field: SerializeField] public uint WaveSpawnerStartRadius { get; private set; }
    [field: SerializeField] public uint WaveSpawnerDistanceBetweenCircles { get; private set; }
    [field: SerializeField] public uint WaveSpawnerStartAngleStep { get; private set; }
    [field: SerializeField] public AnimationCurve WaveSpawnerAmount { get; private set; }
    [field: SerializeField] public AnimationCurve WaveSpawnerAmountInOnWave { get; private set; }
}
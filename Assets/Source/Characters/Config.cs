using UnityEngine;

[CreateAssetMenu (fileName = "NewConfig", menuName = "ScriptableObjects/Config")]
public class Config : ScriptableObject
{
    [field: SerializeField] public int CharacterHealth { get; private set; }
    [field: SerializeField] public uint CharacterLevelUp { get; private set; }
    [field: SerializeField] public int BotHealth { get; private set; }
    [field: SerializeField] public uint RewardForBot { get; private set; }
    [field: SerializeField] public uint WaveSpawnerStartRadius { get; private set; }
    [field: SerializeField] public uint WaveSpawnerDistanceBetweenCircles { get; private set; }
    [field: SerializeField] public uint WaveSpawnerStartAngleStep { get; private set; }
}
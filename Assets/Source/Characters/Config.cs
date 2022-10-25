using UnityEngine;

[CreateAssetMenu (fileName = "NewConfig", menuName = "ScriptableObjects/Config")]
public class Config : ScriptableObject
{
    [field: SerializeField] public int PlayerHealth { get; private set; }
}
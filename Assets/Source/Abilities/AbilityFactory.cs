using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private Ability[] _abilities;
    [SerializeField] private GetNearbyBot _nearbyBot;
    [SerializeField] private Transform _startPoint;

    private void Start()
    {
        foreach (Ability ability in _abilities)
        {
            Ability newAbility = Instantiate(ability, transform);
            newAbility.Init(_nearbyBot, _startPoint);
        }
    }
}
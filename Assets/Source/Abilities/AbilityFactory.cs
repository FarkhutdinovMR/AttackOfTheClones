using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private List<State> _baseStates;
    [SerializeField] private Ability[] _abilitiesTemplate;
    [SerializeField] private GetNearbyBot _nearbyBot;

    private List<Ability> _abilities = new();

    public IEnumerable<State> BaseStates => _baseStates;
    public IEnumerable<Ability> Abilities => _abilities;

    private void Awake()
    {
        foreach (Ability ability in _abilitiesTemplate)
        {
            Ability newAbility = Instantiate(ability, transform);
            newAbility.Init(_baseStates, _nearbyBot);
            _abilities.Add(newAbility);
        }
    }
}
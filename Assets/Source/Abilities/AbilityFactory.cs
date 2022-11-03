using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] private Ability[] _abilitiesTemplate;
    [SerializeField] private Character _character;
    [SerializeField] private GetNearbyBot _targetSource;

    private List<Ability> _abilities = new();

    public Slot[] Slots { get; private set; }
    public IEnumerable<Ability> Abilities => _abilities;

    public void Start()
    {
        Slots = new Slot[_abilitiesTemplate.Length];

        for (int i = 0; i < _abilitiesTemplate.Length; i++)
        {
            Ability newAbility = Instantiate(_abilitiesTemplate[i], transform);
            _abilities.Add(newAbility);
            Slots[i] = new Slot(_character.States.Concat(newAbility.States).ToList());
            newAbility.Init(Slots[i], _targetSource);
        }

        foreach (IAbility ability in _abilities)
            ability.Use();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class IcePuddleAbilityFactory : MonoBehaviour
{
    [SerializeField] private StateConfig _damage;
    [SerializeField] private StateConfig _radius;
    [SerializeField] private StateConfig _interval;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Weapon _weapon;

    public IcePuddleAbility Create()
    {
        var states = new List<State>() { new AttackDamage(_damage), new AttackRadius(_radius), new AttackInterval(_interval) };
        return new IcePuddleAbility(states, _icon, _weapon);
    }
}
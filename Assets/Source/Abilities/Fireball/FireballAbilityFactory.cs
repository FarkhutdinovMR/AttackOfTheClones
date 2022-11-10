using System.Collections.Generic;
using UnityEngine;

public class FireballAbilityFactory : MonoBehaviour
{
    [SerializeField] private StateConfig _damage;
    [SerializeField] private StateConfig _radius;
    [SerializeField] private StateConfig _interval;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Weapon _weapon;

    public FireballAbility Create()
    {
        var states = new List<State>() { new AttackDamage(_damage), new AttackRadius(_radius), new AttackInterval(_interval) };
        return new FireballAbility(states, _icon, _weapon);
    }
}
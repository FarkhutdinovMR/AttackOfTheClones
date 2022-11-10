using System;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    [SerializeField] FireballAbilityFactory _fireballFactory;
    [SerializeField] IcePuddleAbilityFactory _icePuddleFactory;
    [SerializeField] BallLightningFactory _ballLightningFactory;

    public Ability CreateAbility<T>() where T : Ability
    {
        if (typeof(T) == typeof(FireballAbility))
            return _fireballFactory.Create();
        else if (typeof(T) == typeof(IcePuddleAbility))
            return _icePuddleFactory.Create();
        else if (typeof(T) == typeof(BallLightningAbility))
            return _ballLightningFactory.Create();

        return null;
    }

    public Ability CreateAbility(Type type)
    {
        if (type == typeof(FireballAbility))
            return _fireballFactory.Create();
        else if (type == typeof(IcePuddleAbility))
            return _icePuddleFactory.Create();
        else if (type == typeof(BallLightningAbility))
            return _ballLightningFactory.Create();

        return null;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IcePuddleAbility : Ability
{
    public IcePuddleAbility(List<State> states, Sprite icon, Weapon weapon) : base(states, icon, weapon)
    {
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireballAbility : Ability
{
    public FireballAbility(List<State> states, Sprite icon, Weapon weapon) : base(states, icon, weapon) { }
}
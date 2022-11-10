using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BallLightningAbility : Ability
{
    public BallLightningAbility(List<State> states, Sprite icon, Weapon weapon) : base(states, icon, weapon) { }
}
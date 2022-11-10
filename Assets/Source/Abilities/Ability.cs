using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ability
{
    [field: SerializeReference] public List<State> States { get; private set; }
    [field: SerializeReference] public Weapon Weapon { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    protected Ability(List<State> states, Sprite icon, Weapon weapon)
    {
        States = states ?? throw new ArgumentNullException(nameof(states));
        Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
    }
}
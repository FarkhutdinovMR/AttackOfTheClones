using System;

[Serializable]
public class FireballAttackInterval : State
{
    public FireballAttackInterval(StateConfig config, uint level) : base(config, level) { }
}
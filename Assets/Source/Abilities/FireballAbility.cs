using System.Collections;
using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private Fireball _fireballTemplate;

    public override IEnumerator Use()
    {
        Vector3 direction = Target.position - transform.position;
        Fireball newFireball = Instantiate(_fireballTemplate, transform.position + Vector3.up, Quaternion.LookRotation(direction));
        newFireball.Init(AttackRadius, AttackDamage);

        yield return null;
    }
}
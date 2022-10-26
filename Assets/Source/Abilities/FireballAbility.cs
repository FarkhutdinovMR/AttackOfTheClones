using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private Fireball _fireballTemplate;

    protected override void Attack(Transform target) 
    {
        Vector3 direction = target.position - transform.position;
        Fireball newFireball = Instantiate(_fireballTemplate, StartPoint.position, Quaternion.LookRotation(direction));
        newFireball.Init(Radius, Damage);
    }
}
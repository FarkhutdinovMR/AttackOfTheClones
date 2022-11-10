using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class FireballWeapon : Weapon
{
    [SerializeField] private Fireball _fireballTemplate;

    public override void Use()
    {
        StartCoroutine(Repeat());
    }

    private IEnumerator Repeat()
    {
        while (true)
        {
            yield return null;

            if (TargetSource.Target == null)
                continue;

            Vector3 direction = TargetSource.Target.position - transform.position;
            Fireball newFireball = Instantiate(_fireballTemplate, transform.position + Vector3.up, Quaternion.LookRotation(direction));
            newFireball.Init(Slot.GetValue(typeof(AttackRadius)), (int)Slot.GetValue(typeof(AttackDamage)));

            yield return new WaitForSeconds(Slot.GetValue(typeof(AttackInterval)));
        }
    }
}
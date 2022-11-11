using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class FireballAbility : Ability
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
            newFireball.Init(Slot.GetValue(StateType.Radius), (int)Slot.GetValue(StateType.Damage));

            yield return new WaitForSeconds(Slot.GetValue(StateType.Interval));
        }
    }
}
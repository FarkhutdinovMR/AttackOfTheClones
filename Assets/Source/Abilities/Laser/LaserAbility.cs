using System.Collections;
using UnityEngine;

public class LaserAbility : Ability
{
    [SerializeField] private Laser _laserTemplate;

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
            Laser newLaser = Instantiate(_laserTemplate, transform.position + Vector3.up, Quaternion.LookRotation(direction));
            newLaser.Init((int)Slot.GetValue(StateType.Damage));

            yield return new WaitForSeconds(Slot.GetValue(StateType.Interval));
        }
    }
}
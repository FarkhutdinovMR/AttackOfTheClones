using System.Collections;
using UnityEngine;

public class BallLightningWeapon : Weapon
{
    [SerializeField] private BallLightning _ballLightningTemplate;

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
            BallLightning newBallLightning = Instantiate(_ballLightningTemplate, transform.position + Vector3.up, Quaternion.LookRotation(direction));
            newBallLightning.Init(Slot.GetValue(typeof(AttackRadius)), (int)Slot.GetValue(typeof(AttackDamage)));

            yield return new WaitForSeconds(Slot.GetValue(typeof(AttackInterval)));
        }
    }
}
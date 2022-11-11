using System.Collections;
using UnityEngine;

public class BallLightningAbility : Ability
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
            newBallLightning.Init(Slot.GetValue(StateType.Radius), (int)Slot.GetValue(StateType.Damage));

            yield return new WaitForSeconds(Slot.GetValue(StateType.Interval));
        }
    }
}
using System.Collections;
using UnityEngine;

public class BallLightningAbility : Ability, IAbility
{
    [SerializeField] private BallLightning _ballLightningTemplate;

    protected override void Init(Saver.Data data)
    {
        States.Add(new BallLightningAttackDamage(StateConfigs.Find(config => config.Type == StateType.AttackDamage), data.GetStateLevel(typeof(BallLightningAttackDamage))));
        States.Add(new BallLightningAttackInterval(StateConfigs.Find(config => config.Type == StateType.AttackInterval), data.GetStateLevel(typeof(BallLightningAttackInterval))));
        States.Add(new BallLightningAttackRadius(StateConfigs.Find(config => config.Type == StateType.AttackRadius), data.GetStateLevel(typeof(BallLightningAttackRadius))));
    }

    public void Use()
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
            newBallLightning.Init(Slot.GetValue(StateType.AttackRadius), (int)Slot.GetValue(StateType.AttackDamage));

            yield return new WaitForSeconds(Slot.GetValue(StateType.AttackInterval));
        }
    }
}
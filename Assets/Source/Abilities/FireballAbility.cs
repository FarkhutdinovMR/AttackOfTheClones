using System.Collections;
using UnityEngine;

public class FireballAbility : Ability, IAbility
{
    [SerializeField] private Fireball _fireballTemplate;

    protected override void Init(Saver.Data data)
    {
        States.Add(new FireballAttackDamage(StateConfigs.Find(config => config.Type == StateType.AttackDamage), data.GetStateLevel(typeof(FireballAttackDamage))));
        States.Add(new FireballAttackInterval(StateConfigs.Find(config => config.Type == StateType.AttackInterval), data.GetStateLevel(typeof(FireballAttackInterval))));
        States.Add(new FireballAttackRadius(StateConfigs.Find(config => config.Type == StateType.AttackRadius), data.GetStateLevel(typeof(FireballAttackRadius))));
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
            Fireball newFireball = Instantiate(_fireballTemplate, transform.position + Vector3.up, Quaternion.LookRotation(direction));
            newFireball.Init(Slot.GetValue(StateType.AttackRadius), (int)Slot.GetValue(StateType.AttackDamage));

            yield return new WaitForSeconds(Slot.GetValue(StateType.AttackInterval));
        }
    }
}
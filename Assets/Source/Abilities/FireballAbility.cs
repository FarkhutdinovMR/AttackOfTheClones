using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FireballAbility : Ability, IAbility
{
    [SerializeField] private Fireball _fireballTemplate;
    [field: SerializeField] public FireballState[] States { get; private set; }

    public override IEnumerable<State> BaseStates => States;

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
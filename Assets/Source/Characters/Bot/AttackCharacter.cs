using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AttackCharacter : Action
{
    public int Damage;
    public float Distance;
    public SharedCharacter Character;

    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(transform.position, Character.Value.transform.position);

        if (distance <= Distance && Character.Value.TryGetComponent(out Health health))
            health.TakeDamage(Damage);

        return TaskStatus.Success;
    }
}
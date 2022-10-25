using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckDistanceToObject<TObject, TSharedObject> : Conditional where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public float Distance;
    public TSharedObject TargetObject;

    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(transform.position, TargetObject.Value.transform.position);

        if (distance <= Distance)
            return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChaseObject<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public SharedBotInput SelfBotInput;
    public TSharedObject TargetObject;

    public override TaskStatus OnUpdate()
    {
        Vector3 positionDifference = TargetObject.Value.transform.position - transform.position;
        positionDifference.y = 0f;
        Vector3 chaseDirection = Vector3.Normalize(positionDifference);
        SelfBotInput.Value.MovementInput = new Vector2(chaseDirection.x, chaseDirection.z);
        return TaskStatus.Running;
    }
}